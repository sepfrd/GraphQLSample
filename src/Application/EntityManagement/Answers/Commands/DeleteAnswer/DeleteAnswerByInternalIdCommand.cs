using Application.Common.Commands;

namespace Application.EntityManagement.Answers.Commands.DeleteAnswer;

public record DeleteAnswerByInternalIdCommand(Guid Id) : BaseDeleteByInternalIdCommand(Id);