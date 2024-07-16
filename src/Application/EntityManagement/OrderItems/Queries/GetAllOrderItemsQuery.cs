using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.OrderItems.Queries;

public record GetAllOrderItemsQuery(Expression<Func<OrderItem, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<OrderItem>>>;