using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.CartItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.CartItems.Handlers;

public sealed class GetAllCartItemsByCartExternalIdQueryHandler : IRequestHandler<GetAllCartItemsByCartExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllCartItemsByCartExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllCartItemsByCartExternalIdQuery request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork.CartRepository.GetByExternalIdAsync(request.CartExternalId, new Func<Cart, object?>[]
            {
                cartEntity => cartEntity.CartItems
            },
            cancellationToken);

        if (cart is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (cart.CartItems is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(GetAllCartItemsByCartExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var cartItemDtos = _mappingService.Map<ICollection<CartItem>, ICollection<CartItemDto>>(cart.CartItems);

        if (cartItemDtos is not null)
        {
            return new QueryResponse
                (
                cartItemDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(CartItem), typeof(GetAllCartItemsByCartExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}