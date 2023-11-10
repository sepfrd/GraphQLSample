using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.CartItems.Queries;

public record GetAllCartItemsQuery(
        Pagination Pagination,
        Expression<Func<CartItem, object?>>[]? RelationsToInclude = null,
        Expression<Func<CartItem, bool>>? Filter = null)
    : BaseGetAllQuery<CartItem>(Pagination, RelationsToInclude, Filter);