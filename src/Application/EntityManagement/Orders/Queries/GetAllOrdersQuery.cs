using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Orders.Queries;

public record GetAllOrdersQuery(
        Pagination Pagination,
        Expression<Func<Order, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Order>>>;