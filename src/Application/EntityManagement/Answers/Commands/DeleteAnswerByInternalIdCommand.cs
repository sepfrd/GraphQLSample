using Application.Common.Commands;

namespace Application.EntityManagement.Answers.Commands;

public record DeleteAnswerByInternalIdCommand(Guid InternalId) : BaseDeleteByInternalIdCommand(InternalId);