#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.UserRoles.Commands;

public record DeleteUserRoleByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;