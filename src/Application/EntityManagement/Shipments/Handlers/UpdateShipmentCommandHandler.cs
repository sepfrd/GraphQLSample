using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Shipments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class UpdateShipmentCommandHandler(
        IRepository<Shipment> shipmentRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Shipment, ShipmentDto>(shipmentRepository, mappingService, logger);