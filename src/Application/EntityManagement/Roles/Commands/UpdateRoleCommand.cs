#region

using Application.Common;
using Application.EntityManagement.Roles.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Roles.Commands;

public record UpdateRoleCommand(int ExternalId, RoleDto RoleDto) : IRequest<CommandResult>;