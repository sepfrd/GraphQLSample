using Application.Common;
using Application.EntityManagement.Roles.Dtos;
using MediatR;

namespace Application.EntityManagement.Roles.Commands;

public record CreateRoleCommand(RoleDto RoleDto) : IRequest<CommandResult>;