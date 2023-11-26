using Application.Common;
using Application.EntityManagement.Shipments.Dtos;
using MediatR;

namespace Application.EntityManagement.Shipments.Commands;

public record UpdateShipmentCommand(int ExternalId, ShipmentDto ShipmentDto) : IRequest<CommandResult>;