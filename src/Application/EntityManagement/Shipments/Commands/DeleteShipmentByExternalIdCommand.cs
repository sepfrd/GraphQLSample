using Application.Common;
using MediatR;

namespace Application.EntityManagement.Shipments.Commands;

public record DeleteShipmentByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;