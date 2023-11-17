using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.CartItems.Handlers;

public class DeleteCartItemByExternalICommandHandler(IRepository<CartItem> cartItemRepository)
    : IRequestHandler<DeleteCartItemByExternalICommand, CommandResult>
{
    public async Task<CommandResult> Handle(DeleteCartItemByExternalICommand request, CancellationToken cancellationToken)
    {
        var cartItem = await cartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (cartItem is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        await cartItemRepository.DeleteAsync(cartItem, cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}