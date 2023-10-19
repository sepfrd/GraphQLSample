using Application.Common;
using MediatR;

namespace Application.EntityManagement.CartItems.Queries;

public sealed record GetAllCartItemsByCartExternalIdQuery(int CartExternalId, Pagination Pagination) : IRequest<QueryResponse>;