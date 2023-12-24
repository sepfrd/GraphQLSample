using Application.Common;
using Application.EntityManagement.UserRoles.Dtos.UserRoleDto;
using MediatR;

namespace Application.EntityManagement.UserRoles.Commands;

public record CreateUserRoleCommand(UserRoleDto UserRoleDto) : IRequest<CommandResult>;