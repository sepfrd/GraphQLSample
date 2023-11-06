using Application.Common;
using Application.EntityManagement.Shipments.Dtos;
using MediatR;

namespace Application.EntityManagement.Shipments.Queries;

public record GetShipmentByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryReferenceResponse<ShipmentDto>>;