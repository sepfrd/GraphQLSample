#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.OrderItems.Commands;

public record DeleteOrderItemByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;