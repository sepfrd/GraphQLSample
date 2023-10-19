using Application.Common;
using MediatR;

namespace Application.EntityManagement.Orders.Queries;

public record GetOrdersByUserExternalIdQuery(int UserExternalId) : IRequest<QueryResponse>;