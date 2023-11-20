namespace Application.EntityManagement.UserRoles.Dtos;

public record UserRoleDto(
    int ExternalId,
    Guid UserId,
    Guid RoleId);