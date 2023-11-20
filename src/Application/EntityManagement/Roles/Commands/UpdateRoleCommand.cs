using Application.Common.Commands;
using Application.EntityManagement.Roles.Dtos;

namespace Application.EntityManagement.Roles.Commands;

public record UpdateRoleCommand(int ExternalId, RoleDto Dto) : BaseUpdateCommand<RoleDto>(ExternalId, Dto);