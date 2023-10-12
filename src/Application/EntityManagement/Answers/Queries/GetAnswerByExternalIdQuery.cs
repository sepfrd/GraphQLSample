using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Queries;

public record GetAnswerByExternalIdQuery
(
    int ExternalId,
    IEnumerable<Func<Answer, object?>>? RelationsToInclude = null
) : BaseGetByExternalIdQuery<Answer>(ExternalId, RelationsToInclude);