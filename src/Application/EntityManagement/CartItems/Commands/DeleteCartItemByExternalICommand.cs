using Application.Common;
using MediatR;

namespace Application.EntityManagement.CartItems.Commands;

public record DeleteCartItemByExternalICommand(int ExternalId) : IRequest<CommandResult>;