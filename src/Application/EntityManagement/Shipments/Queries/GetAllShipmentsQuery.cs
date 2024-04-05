using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Shipments.Queries;

public record GetAllShipmentsQuery(Expression<Func<Shipment, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Shipment>>>;