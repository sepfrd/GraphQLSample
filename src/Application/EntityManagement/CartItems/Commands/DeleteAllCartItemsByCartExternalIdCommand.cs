#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.CartItems.Commands;

public record DeleteAllCartItemsByCartExternalIdCommand(int ExternalId) : IRequest<CommandResult>;