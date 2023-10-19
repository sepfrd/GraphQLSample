using Application.Common.Commands;
using Application.EntityManagement.Shipments.Dtos;

namespace Application.EntityManagement.Shipments.Commands;

public record CreateShipmentCommand(ShipmentDto ShipmentDto) : BaseCreateCommand<ShipmentDto>(ShipmentDto);