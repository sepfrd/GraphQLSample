using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Orders.Queries;

public record GetAllOrdersQuery(
        Pagination Pagination,
        Expression<Func<Order, object?>>[]? RelationsToInclude = null,
        Expression<Func<Order, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Order>>>;