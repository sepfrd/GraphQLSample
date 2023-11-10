using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Carts.Queries;

public record GetAllCartsQuery(
        Pagination Pagination,
        Expression<Func<Cart, object?>>[]? RelationsToInclude = null,
        Expression<Func<Cart, bool>>? Filter = null)
    : BaseGetAllQuery<Cart>(Pagination, RelationsToInclude, Filter);