using Application.Common;
using MediatR;

namespace Application.EntityManagement.Carts.Commands;

public record DeleteAllCartItemsByCartExternalIdCommand(int ExternalId) : IRequest<CommandResult>;