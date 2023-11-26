#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Orders.Commands;

public record DeleteOrderByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;