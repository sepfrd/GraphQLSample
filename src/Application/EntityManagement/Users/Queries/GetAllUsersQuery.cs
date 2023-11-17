using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUsersQuery(
        Pagination Pagination,
        Expression<Func<User, object?>>[]? RelationsToInclude = null,
        Expression<Func<User, bool>>? Filter = null)
    : BaseGetAllQuery<User>(Pagination, RelationsToInclude, Filter);