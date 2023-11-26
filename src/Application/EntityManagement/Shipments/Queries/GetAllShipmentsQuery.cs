#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.Shipments.Queries;

public record GetAllShipmentsQuery(
        Pagination Pagination,
        Expression<Func<Shipment, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Shipment>>>;