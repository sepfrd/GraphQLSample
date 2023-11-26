using Application.Common;
using MediatR;

namespace Application.EntityManagement.CartItems.Commands;

public record DeleteAllCartItemsByCartExternalIdCommand(int ExternalId) : IRequest<CommandResult>;