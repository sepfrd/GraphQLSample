using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Payments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, CommandResult>
{
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public UpdatePaymentCommandHandler(
        IRepository<Payment> paymentRepository,
        IRepository<User> userRepository,
        IRepository<Order> orderRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
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
                typeof(UpdatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(UpdatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (order.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var entity = await _paymentRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = _mappingService.Map(request.PaymentDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Payment),
                typeof(UpdatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _paymentRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Payment),
            typeof(UpdatePaymentCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}