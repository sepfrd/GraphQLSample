using Application.Common;
using Application.EntityManagement.Roles.Dtos.RoleDto;
using MediatR;

namespace Application.EntityManagement.Roles.Commands;

public record UpdateRoleCommand(int ExternalId, RoleDto RoleDto) : IRequest<CommandResult>;