using Application.Common.Queries;

namespace Application.EntityManagement.Answers.Queries;

public record GetAnswerByInternalIdQuery(Guid Id)
    : BaseGetByInternalIdQuery(Id);