#region

using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

#endregion

namespace Application.EntityManagement.CartItems.Handlers;

public class DeleteCartItemByExternalICommandHandler : IRequestHandler<DeleteCartItemByExternalICommand, CommandResult>
{
    private readonly IRepository<CartItem> _cartItemRepository;

    public DeleteCartItemByExternalICommandHandler(IRepository<CartItem> cartItemRepository)
    {
        _cartItemRepository = cartItemRepository;
    }

    public async Task<CommandResult> Handle(DeleteCartItemByExternalICommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (cartItem is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        await _cartItemRepository.DeleteAsync(cartItem, cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}