using Application.Common;
using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersQuery
(
    Pagination Pagination,
    IEnumerable<Func<Answer, object?>>? RelationsToInclude = null
) : BaseGetAllQuery<Answer>(Pagination, RelationsToInclude);