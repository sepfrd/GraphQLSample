using Application.Common;
using MediatR;

namespace Application.EntityManagement.Shipments.Queries;

public record GetShipmentByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryResponse>;