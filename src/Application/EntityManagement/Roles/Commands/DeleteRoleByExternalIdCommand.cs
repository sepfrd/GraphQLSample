using Application.Common.Commands;

namespace Application.EntityManagement.Roles.Commands;

public record DeleteRoleByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);