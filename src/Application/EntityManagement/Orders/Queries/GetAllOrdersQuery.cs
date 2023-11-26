#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.Orders.Queries;

public record GetAllOrdersQuery(
        Pagination Pagination,
        Expression<Func<Order, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Order>>>;