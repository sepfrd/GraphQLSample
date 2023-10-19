using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Shipments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class UpdateShipmentCommandHandler : BaseUpdateCommandHandler<Shipment, ShipmentDto>
{
    public UpdateShipmentCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}