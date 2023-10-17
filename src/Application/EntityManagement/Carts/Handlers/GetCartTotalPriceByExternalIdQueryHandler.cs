using Application.Common;
using Application.EntityManagement.Carts.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Carts.Handlers;

public class GetCartTotalPriceByExternalIdQueryHandler : IRequestHandler<GetCartTotalPriceByExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetCartTotalPriceByExternalIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<QueryResponse> Handle(GetCartTotalPriceByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetByExternalIdAsync(request.ExternalId,
            new Func<Cart, object?>[]
            {
                entity => entity.CartItems
            },
            cancellationToken);

        if (cart is null)
        {
            return new QueryResponse
                (
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        return new QueryResponse
            (
            cart.CartItems,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}