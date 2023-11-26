#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.CartItems.Commands;

public record UpdateCartItemQuantityCommand(int ExternalId, int NewQuantity) : IRequest<CommandResult>;