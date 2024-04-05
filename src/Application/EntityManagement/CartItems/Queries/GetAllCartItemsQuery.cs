using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.CartItems.Queries;

public record GetAllCartItemsQuery(Expression<Func<CartItem, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<CartItem>>>;