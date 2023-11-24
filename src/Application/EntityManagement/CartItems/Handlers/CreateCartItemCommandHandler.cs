using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Application.EntityManagement.CartItems.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class CreateCartItemCommandHandler(
        IRepository<CartItem> cartItemRepository,
        IRepository<Cart> cartRepository,
        IRepository<Product> productRepository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateCartItemCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartEntity = await cartRepository.GetByExternalIdAsync(
            request.CartItemDto.CartExternalId,
            cancellationToken);

        if (cartEntity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        if (cartEntity.CartItems is null)
        {
            logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var existingCartItem = cartEntity.CartItems.FirstOrDefault(item => item.Product?.ExternalId == request.CartItemDto.Product.ExternalId);

        if (existingCartItem is not null)
        {
            existingCartItem.Quantity += request.CartItemDto.Quantity;

            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        var cartItem = mappingService.Map<CartItemDto, CartItem>(request.CartItemDto);

        if (cartItem is null)
        {
            logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var productEntity = await productRepository
            .GetByExternalIdAsync(request.CartItemDto.Product.ExternalId, cancellationToken);

        if (productEntity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        cartItem.CartId = cartEntity.InternalId;
        cartItem.ProductId = productEntity.InternalId;

        var createdCartItem = await cartItemRepository.CreateAsync(cartItem, cancellationToken);

        if (createdCartItem is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}