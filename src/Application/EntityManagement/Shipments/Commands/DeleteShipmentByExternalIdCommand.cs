#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Shipments.Commands;

public record DeleteShipmentByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;