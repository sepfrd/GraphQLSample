using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Shipments.Commands;
using Application.EntityManagement.Shipments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class CreateShipmentCommandHandler(
        IRepository<Shipment> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateShipmentCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateShipmentCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<ShipmentDto, Shipment>(request.ShipmentDto);

        if (entity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Shipment), typeof(CreateShipmentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Shipment), typeof(CreateShipmentCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}