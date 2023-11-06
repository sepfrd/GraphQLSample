using Application.Common;
using Application.EntityManagement.Orders.Dtos;
using MediatR;

namespace Application.EntityManagement.Orders.Queries;

public record GetOrdersByUserExternalIdQuery(int UserExternalId) : IRequest<QueryReferenceResponse<IEnumerable<OrderDto>>>;