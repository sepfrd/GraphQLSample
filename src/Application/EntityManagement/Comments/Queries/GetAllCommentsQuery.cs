using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Comments.Queries;

public record GetAllCommentsQuery(
        Pagination Pagination,
        Expression<Func<Comment, object?>>[]? RelationsToInclude = null,
        Expression<Func<Comment, bool>>? Filter = null)
    : BaseGetAllQuery<Comment>(Pagination, RelationsToInclude, Filter);