using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Orders.Queries;

public record GetAllOrdersQuery(
        Pagination Pagination,
        Expression<Func<Order, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Order>>>;