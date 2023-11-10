using Application.Common;
using Application.Common.Queries;
using Domain.Entities;
using System.Linq.Expressions;

namespace Application.EntityManagement.Shipments.Queries;

public record GetAllShipmentsQuery(
        Pagination Pagination,
        Expression<Func<Shipment, object?>>[]? RelationsToInclude = null,
        Expression<Func<Shipment, bool>>? Filter = null)
    : BaseGetAllQuery<Shipment>(Pagination, RelationsToInclude, Filter);