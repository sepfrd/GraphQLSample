#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.CartItems.Queries;

public record GetAllCartItemsQuery(
        Pagination Pagination,
        Expression<Func<CartItem, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<CartItem>>>;