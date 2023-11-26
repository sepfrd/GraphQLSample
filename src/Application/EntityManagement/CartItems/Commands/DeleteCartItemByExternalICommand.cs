#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.CartItems.Commands;

public record DeleteCartItemByExternalICommand(int ExternalId) : IRequest<CommandResult>;