using Application.Common;
using Application.EntityManagement.Carts.Queries;
using Domain.Abstractions;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Carts.Handlers;

public class GetCartTotalPriceByExternalIdQueryHandler : IRequestHandler<GetCartTotalPriceByExternalIdQuery, QueryValueResponse<decimal>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCartTotalPriceByExternalIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<QueryValueResponse<decimal>> Handle(GetCartTotalPriceByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork
            .CartRepository
            .GetByExternalIdAsync(request.ExternalId, cancellationToken,
                entity => entity.CartItems);

        if (cart is null)
        {
            return new QueryValueResponse<decimal>(
                default,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound);
        }

        return new QueryValueResponse<decimal>(
            cart.TotalPrice,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}