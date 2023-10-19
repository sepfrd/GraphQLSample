using Application.Common;
using MediatR;

namespace Application.EntityManagement.OrderItems.Queries;

public record GetAllOrderItemsByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryResponse>;