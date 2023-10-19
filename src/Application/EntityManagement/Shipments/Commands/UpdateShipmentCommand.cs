using Application.Common.Commands;
using Application.EntityManagement.Shipments.Dtos;

namespace Application.EntityManagement.Shipments.Commands;

public record UpdateShipmentCommand(int ExternalId, ShipmentDto ShipmentDto)
    : BaseUpdateCommand<ShipmentDto>(ExternalId, ShipmentDto);