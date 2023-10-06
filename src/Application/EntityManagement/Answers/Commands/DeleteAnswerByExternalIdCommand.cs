using Application.Common.Commands;

namespace Application.EntityManagement.Answers.Commands;

public record DeleteAnswerByExternalIdCommand(int Id) : BaseDeleteByExternalIdCommand(Id);