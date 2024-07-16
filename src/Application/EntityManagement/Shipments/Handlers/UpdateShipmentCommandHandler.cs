using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Shipments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class UpdateShipmentCommandHandler : IRequestHandler<UpdateShipmentCommand, CommandResult>
{
    private readonly IRepository<Shipment> _shipmentRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public UpdateShipmentCommandHandler(
        IRepository<Shipment> shipmentRepository,
        IRepository<User> userRepository,
        IRepository<Order> orderRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _shipmentRepository = shipmentRepository;
        _mappingService = mappingService;
        _logger = logger;
        _authenticationService = authenticationService;
        _userRepository = userRepository;
        _orderRepository = orderRepository;
    }

    public virtual async Task<CommandResult> Handle(UpdateShipmentCommand request, CancellationToken cancellationToken)
    {
        var entity = await _shipmentRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var order = await _orderRepository.GetByInternalIdAsync(entity.OrderId, cancellationToken);

        if (order is null)
        {
            _logger.LogError(MessageConstants.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Shipment),
                typeof(UpdateShipmentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(UpdateShipmentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(UpdateShipmentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (order.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var newEntity = _mappingService.Map(request.ShipmentDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Shipment),
                typeof(UpdateShipmentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _shipmentRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Shipment),
            typeof(UpdateShipmentCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}