using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Queries;

public record GetAnswerByInternalIdQuery
(
    Guid InternalId,
    IEnumerable<Func<Answer, object?>>? RelationsToInclude = null
) : BaseGetByInternalIdQuery<Answer>(InternalId, RelationsToInclude);