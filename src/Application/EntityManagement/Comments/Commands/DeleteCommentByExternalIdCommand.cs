using Application.Common.Commands;

namespace Application.EntityManagement.Comments.Commands;

public record DeleteCommentByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);