using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.CartItems.Queries;

public record GetAllCartItemsQuery(
        Pagination Pagination,
        Expression<Func<CartItem, object?>>[]? RelationsToInclude = null,
        Expression<Func<CartItem, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<CartItem>>>;