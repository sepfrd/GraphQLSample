using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.CartItems.Queries;

public record GetAllCartItemsQuery(
        Pagination Pagination,
        Expression<Func<CartItem, object?>>[]? RelationsToInclude = null,
        Expression<Func<CartItem, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<CartItem>>>;