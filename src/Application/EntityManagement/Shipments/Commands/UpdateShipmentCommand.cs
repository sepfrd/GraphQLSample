using Application.Common;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Shipments.Dtos.ShipmentDto;
using MediatR;

namespace Application.EntityManagement.Shipments.Commands;

public record UpdateShipmentCommand(int ExternalId, ShipmentDto ShipmentDto) : IRequest<CommandResult>;