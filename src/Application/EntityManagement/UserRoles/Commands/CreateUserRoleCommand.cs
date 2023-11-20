using Application.Common.Commands;
using Application.EntityManagement.UserRoles.Dtos;

namespace Application.EntityManagement.UserRoles.Commands;

public record CreateUserRoleCommand(UserRoleDto Dto) : BaseCreateCommand<UserRoleDto>(Dto);