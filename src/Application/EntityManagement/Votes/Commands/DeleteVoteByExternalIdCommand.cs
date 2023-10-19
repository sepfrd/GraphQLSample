using Application.Common.Commands;

namespace Application.EntityManagement.Votes.Commands;

public record DeleteVoteByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);