using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Shipments.Queries;

public record GetAllShipmentsQuery(
        Pagination Pagination,
        Expression<Func<Shipment, object?>>[]? RelationsToInclude = null,
        Expression<Func<Shipment, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Shipment>>>;