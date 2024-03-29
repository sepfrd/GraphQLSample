using Application.Common;
using MediatR;

namespace Application.EntityManagement.CartItems.Commands;

public record DeleteCartItemByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;