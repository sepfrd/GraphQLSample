using Application.Common.Commands;
using Application.EntityManagement.Roles.Dtos;

namespace Application.EntityManagement.Roles.Commands;

public record CreateRoleCommand(RoleDto Dto) : BaseCreateCommand<RoleDto>(Dto);