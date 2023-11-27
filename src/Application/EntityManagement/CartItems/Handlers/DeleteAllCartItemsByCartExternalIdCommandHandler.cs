using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class DeleteAllCartItemsByCartExternalIdCommandHandler : IRequestHandler<DeleteAllCartItemsByCartExternalIdCommand, CommandResult>
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRepository<Cart> _cartRepository;
    private readonly ILogger _logger;

    public DeleteAllCartItemsByCartExternalIdCommandHandler(IRepository<CartItem> cartItemRepository,
        IRepository<Cart> cartRepository,
        ILogger logger)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(DeleteAllCartItemsByCartExternalIdCommand request, CancellationToken cancellationToken)
    {
        var cart = await _cartRepository.GetByExternalIdAsync(
            request.ExternalId,
            cancellationToken);

        if (cart is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var cartItems = cart.CartItems?.ToList();

        if (cartItems is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(DeleteAllCartItemsByCartExternalIdCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        if (cartItems.Count == 0)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        foreach (var cartItem in cartItems)
        {
            await _cartItemRepository.DeleteOneAsync(cartItem, cancellationToken);
        }

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}