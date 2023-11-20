using Application.Common.Commands;
using Application.EntityManagement.UserRoles.Dtos;

namespace Application.EntityManagement.UserRoles.Commands;

public record UpdateUserRoleCommand(int ExternalId, UserRoleDto Dto)
    : BaseUpdateCommand<UserRoleDto>(ExternalId, Dto);