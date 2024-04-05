using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Payments.Commands;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class CreatePaymentCommandHandler : IRequestHandler<CreatePaymentCommand, CommandResult>
{
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreatePaymentCommandHandler(
        IRepository<Payment> paymentRepository,
        IRepository<Order> orderRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _paymentRepository = paymentRepository;
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByExternalIdAsync(request.PaymentDto.OrderExternalId, cancellationToken);

        if (order is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(CreatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(CreatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (order.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var entity = _mappingService.Map<PaymentDto, Payment>(request.PaymentDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Payment),
                typeof(CreatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.OrderId = order.InternalId;

        var createdEntity = await _paymentRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Payment),
            typeof(CreatePaymentCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}