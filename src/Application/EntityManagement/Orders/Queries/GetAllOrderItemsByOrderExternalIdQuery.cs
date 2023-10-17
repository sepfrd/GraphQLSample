using Application.Common;
using MediatR;

namespace Application.EntityManagement.Orders.Queries;

public record GetAllOrderItemsByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryResponse>;