#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Users.Commands;

public sealed record DeleteUserByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;