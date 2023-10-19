using Application.Common.Commands;

namespace Application.EntityManagement.Questions.Commands;

public record DeleteQuestionByExternalIdCommand(int ExternalId) : BaseDeleteByExternalIdCommand(ExternalId);