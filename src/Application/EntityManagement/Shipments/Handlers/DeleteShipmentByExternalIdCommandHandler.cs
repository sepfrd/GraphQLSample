using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class DeleteShipmentByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Shipment>
{
    public DeleteShipmentByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}