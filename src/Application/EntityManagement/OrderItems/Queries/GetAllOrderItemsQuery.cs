using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.OrderItems.Queries;

public record GetAllOrderItemsQuery(
        Pagination Pagination,
        Expression<Func<OrderItem, object?>>[]? RelationsToInclude = null,
        Expression<Func<OrderItem, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<OrderItem>>>;