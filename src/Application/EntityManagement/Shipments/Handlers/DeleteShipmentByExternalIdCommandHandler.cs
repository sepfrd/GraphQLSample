using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class DeleteShipmentByExternalIdCommandHandler(
        IRepository<Shipment> shipmentRepository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Shipment>(shipmentRepository, logger);