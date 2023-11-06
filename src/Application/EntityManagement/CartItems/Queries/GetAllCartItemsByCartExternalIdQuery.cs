using Application.Common;
using Application.EntityManagement.CartItems.Dtos;
using MediatR;

namespace Application.EntityManagement.CartItems.Queries;

public sealed record GetAllCartItemsByCartExternalIdQuery(int CartExternalId, Pagination Pagination) : IRequest<QueryReferenceResponse<IEnumerable<CartItemDto>>>;