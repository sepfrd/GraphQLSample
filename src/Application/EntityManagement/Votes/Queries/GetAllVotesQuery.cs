using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Votes.Queries;

public record GetAllVotesQuery(
        Pagination Pagination,
        Expression<Func<Vote, object?>>[]? RelationsToInclude = null,
        Expression<Func<Vote, bool>>? Filter = null)
    : BaseGetAllQuery<Vote>(Pagination, RelationsToInclude, Filter);