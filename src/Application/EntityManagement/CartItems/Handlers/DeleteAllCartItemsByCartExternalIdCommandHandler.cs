using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class DeleteAllCartItemsByCartExternalIdCommandHandler(
        IRepository<CartItem> cartItemRepository,
        IRepository<Cart> cartRepository,
        ILogger logger)
    : IRequestHandler<DeleteAllCartItemsByCartExternalIdCommand, CommandResult>
{

    public async Task<CommandResult> Handle(DeleteAllCartItemsByCartExternalIdCommand request, CancellationToken cancellationToken)
    {
        var cart = await cartRepository.GetByExternalIdAsync(
            request.ExternalId,
            cancellationToken,
            cartEntity => cartEntity.CartItems);

        if (cart is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var cartItems = cart.CartItems?.ToList();

        if (cartItems is null)
        {
            logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(DeleteAllCartItemsByCartExternalIdCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        if (cartItems.Count == 0)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        foreach (var cartItem in cartItems)
        {
            await cartItemRepository.DeleteAsync(cartItem, cancellationToken);
        }

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}