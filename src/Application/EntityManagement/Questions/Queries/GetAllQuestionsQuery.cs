using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Questions.Queries;

public record GetAllQuestionsQuery(
        Pagination Pagination,
        Expression<Func<Question, object?>>[]? RelationsToInclude = null,
        Expression<Func<Question, bool>>? Filter = null)
    : BaseGetAllQuery<Question>(Pagination, RelationsToInclude, Filter);