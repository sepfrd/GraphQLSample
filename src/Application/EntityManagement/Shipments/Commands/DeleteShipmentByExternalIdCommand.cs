using Application.Common.Commands;

namespace Application.EntityManagement.Shipments.Commands;

public record DeleteShipmentByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);