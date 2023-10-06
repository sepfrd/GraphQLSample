using Application.Common.Commands;

namespace Application.EntityManagement.Answers.Commands.DeleteAnswer;

public record DeleteAnswerByExternalIdCommand(int Id) : BaseDeleteByExternalIdCommand(Id);