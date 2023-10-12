using Application.Common;
using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswerDtosQuery
(
    Pagination Pagination,
    IEnumerable<Func<Answer, object?>>? RelationsToInclude = null
) : BaseGetAllDtosQuery<Answer>(Pagination, RelationsToInclude);