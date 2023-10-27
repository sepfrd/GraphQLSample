using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class DeleteAllCartItemsByCartExternalIdCommandHandler : IRequestHandler<DeleteAllCartItemsByCartExternalIdCommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public DeleteAllCartItemsByCartExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(DeleteAllCartItemsByCartExternalIdCommand request, CancellationToken cancellationToken)
    {
        var cart = await _unitOfWork
            .CartRepository
            .GetByExternalIdAsync(request.ExternalId, cancellationToken,
                cartEntity => cartEntity.CartItems);

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
            await _unitOfWork.CartItemRepository.DeleteAsync(cartItem, cancellationToken);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}