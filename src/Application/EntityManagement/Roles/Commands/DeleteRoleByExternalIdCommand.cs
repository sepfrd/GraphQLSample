#region

using Application.Common;
using MediatR;

#endregion

namespace Application.EntityManagement.Roles.Commands;

public record DeleteRoleByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;