using Application.Common;
using MediatR;

namespace Application.EntityManagement.Orders.Commands;

public record DeleteOrderByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;