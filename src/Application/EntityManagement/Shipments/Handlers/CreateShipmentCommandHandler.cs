using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Shipments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class CreateShipmentCommandHandler(
        IRepository<Shipment> shipmentRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Shipment, ShipmentDto>(shipmentRepository, mappingService, logger);