#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Users.Commands;

public sealed record DeleteUserByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;