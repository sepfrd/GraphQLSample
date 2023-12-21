using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Carts.Queries;

public record GetAllCartsQuery(
        Pagination Pagination,
        Expression<Func<Cart, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Cart>>>;