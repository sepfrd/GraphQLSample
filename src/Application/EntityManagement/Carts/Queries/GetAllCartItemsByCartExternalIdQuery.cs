using Application.Common;
using MediatR;

namespace Application.EntityManagement.Carts.Queries;

public sealed record GetAllCartItemsByCartExternalIdQuery(int CartExternalId) : IRequest<QueryResponse>;