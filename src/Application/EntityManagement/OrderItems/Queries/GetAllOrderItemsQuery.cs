using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.OrderItems.Queries;

public record GetAllOrderItemsQuery(
        Pagination Pagination,
        Expression<Func<OrderItem, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<OrderItem>>>;