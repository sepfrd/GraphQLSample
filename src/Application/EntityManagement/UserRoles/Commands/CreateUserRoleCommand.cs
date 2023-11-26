using Application.Common;
using Application.EntityManagement.UserRoles.Dtos;
using MediatR;

namespace Application.EntityManagement.UserRoles.Commands;

public record CreateUserRoleCommand(UserRoleDto UserRoleDto) : IRequest<CommandResult>;