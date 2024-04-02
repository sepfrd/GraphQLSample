using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Carts.Queries;

public record GetAllCartsQuery(
        Pagination Pagination,
        Expression<Func<Cart, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Cart>>>;