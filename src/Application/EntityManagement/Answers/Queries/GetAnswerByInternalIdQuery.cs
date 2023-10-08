using Application.Common.Queries;

namespace Application.EntityManagement.Answers.Queries;

public record GetAnswerByInternalIdQuery(Guid InternalId)
    : BaseGetByInternalIdQuery(InternalId);