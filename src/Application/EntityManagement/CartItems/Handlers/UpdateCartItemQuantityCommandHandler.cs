using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CommandResult>
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly ILogger _logger;

    public UpdateCartItemQuantityCommandHandler(IRepository<CartItem> cartItemRepository, ILogger logger)
    {
        _cartItemRepository = cartItemRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (cartItem is null || cartItem.Quantity == request.NewQuantity)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        cartItem.Quantity = request.NewQuantity;

        var newCartItem = await _cartItemRepository.UpdateAsync(cartItem, cancellationToken);

        if (newCartItem is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(CartItem), typeof(UpdateCartItemQuantityCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}