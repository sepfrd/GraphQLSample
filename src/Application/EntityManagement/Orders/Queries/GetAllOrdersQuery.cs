using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Orders.Queries;

public record GetAllOrdersQuery(
        Pagination Pagination,
        Expression<Func<Order, object?>>[]? RelationsToInclude = null,
        Expression<Func<Order, bool>>? Filter = null)
    : BaseGetAllQuery<Order>(Pagination, RelationsToInclude, Filter);