using Application.Common;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using MediatR;

namespace Application.EntityManagement.CartItems.Handlers;

public class DeleteCartItemByExternalICommandHandler : IRequestHandler<DeleteCartItemByExternalICommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;

    public DeleteCartItemByExternalICommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    
    public async Task<CommandResult> Handle(DeleteCartItemByExternalICommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _unitOfWork.CartItemRepository.GetByExternalIdAsync(request.ExternalId, null, cancellationToken);

        if (cartItem is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        await _unitOfWork.CartItemRepository.DeleteAsync(cartItem, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}