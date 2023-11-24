using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Shipments.Queries;

public record GetAllShipmentsQuery(
        Pagination Pagination,
        Expression<Func<Shipment, object?>>[]? RelationsToInclude = null,
        Expression<Func<Shipment, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Shipment>>>;