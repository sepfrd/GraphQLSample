#region

using Application.Common;
using Application.EntityManagement.Shipments.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Shipments.Commands;

public record CreateShipmentCommand(ShipmentDto ShipmentDto) : IRequest<CommandResult>;