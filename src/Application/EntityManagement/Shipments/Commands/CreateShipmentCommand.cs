using Application.Common;
using Application.EntityManagement.Shipments.Dtos;
using MediatR;

namespace Application.EntityManagement.Shipments.Commands;

public record CreateShipmentCommand(ShipmentDto ShipmentDto) : IRequest<CommandResult>;