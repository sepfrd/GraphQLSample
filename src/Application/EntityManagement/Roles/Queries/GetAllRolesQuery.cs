using System.Linq.Expressions;
using Application.Common;
using Application.Common.Queries;
using Domain.Entities;

namespace Application.EntityManagement.Roles.Queries;

public record GetAllRolesQuery(
        Pagination Pagination,
        Expression<Func<Role, object?>>[]? RelationsToInclude = null,
        Expression<Func<Role, bool>>? Filter = null)
    : BaseGetAllQuery<Role>(Pagination, RelationsToInclude, Filter);