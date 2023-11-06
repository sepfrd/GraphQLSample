using Application.Common;
using Application.EntityManagement.OrderItems.Dtos;
using MediatR;

namespace Application.EntityManagement.OrderItems.Queries;

public record GetAllOrderItemsByOrderExternalIdQuery(int OrderExternalId) : IRequest<QueryReferenceResponse<IEnumerable<OrderItemDto>>>;