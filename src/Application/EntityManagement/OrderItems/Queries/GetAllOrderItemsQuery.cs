using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.OrderItems.Queries;

public record GetAllOrderItemsQuery(
        Pagination Pagination,
        Expression<Func<OrderItem, object?>>[]? RelationsToInclude = null,
        Expression<Func<OrderItem, bool>>? Filter = null)
    : BaseGetAllQuery<OrderItem>(Pagination, RelationsToInclude, Filter);