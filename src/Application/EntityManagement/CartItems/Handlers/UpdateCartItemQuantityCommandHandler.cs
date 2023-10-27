using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public UpdateCartItemQuantityCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _unitOfWork.CartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (cartItem is null || cartItem.Quantity == request.NewQuantity)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        cartItem.Quantity = request.NewQuantity;

        var newCartItem = await _unitOfWork.CartItemRepository.UpdateAsync(cartItem, cancellationToken);

        if (newCartItem is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        _logger.LogError(Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(CartItem), typeof(UpdateCartItemQuantityCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}