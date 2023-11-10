using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Answers.Queries;

public record GetAllAnswersQuery(
        Pagination Pagination,
        Expression<Func<Answer, object?>>[]? RelationsToInclude = null,
        Expression<Func<Answer, bool>>? Filter = null)
    : BaseGetAllQuery<Answer>(Pagination, RelationsToInclude, Filter);