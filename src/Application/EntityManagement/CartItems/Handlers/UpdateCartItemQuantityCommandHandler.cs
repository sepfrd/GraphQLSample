using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class UpdateCartItemQuantityCommandHandler(IRepository<CartItem> cartItemRepository, ILogger logger)
    : IRequestHandler<UpdateCartItemQuantityCommand, CommandResult>
{
    public async Task<CommandResult> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (cartItem is null || cartItem.Quantity == request.NewQuantity)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        cartItem.Quantity = request.NewQuantity;

        var newCartItem = await cartItemRepository.UpdateAsync(cartItem, cancellationToken);

        if (newCartItem is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(CartItem), typeof(UpdateCartItemQuantityCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}