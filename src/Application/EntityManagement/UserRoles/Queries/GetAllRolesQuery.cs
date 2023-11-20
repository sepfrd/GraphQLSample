using System.Linq.Expressions;
using Application.Common;
using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.UserRoles.Queries;

public record GetAllUserRolesQuery(
        Pagination Pagination,
        Expression<Func<UserRole, object?>>[]? RelationsToInclude = null,
        Expression<Func<UserRole, bool>>? Filter = null)
    : BaseGetAllQuery<UserRole>(Pagination, RelationsToInclude, Filter);