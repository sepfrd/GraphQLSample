using Application.Common;
using MediatR;

namespace Application.EntityManagement.CartItems.Commands;

public record UpdateCartItemQuantityCommand(int ExternalId, int NewQuantity) : IRequest<CommandResult>;