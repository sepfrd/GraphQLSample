using Application.Common;
using MediatR;

namespace Application.EntityManagement.Carts.Queries;

public record GetCartTotalPriceByExternalIdQuery(int ExternalId) : IRequest<QueryValueResponse<decimal>>;