using Application.Common.Commands;

namespace Application.EntityManagement.Addresses.Commands;

public record DeleteAddressByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);