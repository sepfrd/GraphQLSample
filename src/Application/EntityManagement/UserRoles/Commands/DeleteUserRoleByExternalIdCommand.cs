using Application.Common.Commands;

namespace Application.EntityManagement.UserRoles.Commands;

public record DeleteUserRoleByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);