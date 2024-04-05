using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Orders.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, CommandResult>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public UpdateOrderCommandHandler(
        IRepository<Order> orderRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _orderRepository = orderRepository;
        _mappingService = mappingService;
        _logger = logger;
        _authenticationService = authenticationService;
        _userRepository = userRepository;
    }

    public virtual async Task<CommandResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _orderRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(UpdateOrderCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(UpdateOrderCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var newEntity = _mappingService.Map(request.CreateOrderDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Order),
                typeof(UpdateOrderCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _orderRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Order),
            typeof(UpdateOrderCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}