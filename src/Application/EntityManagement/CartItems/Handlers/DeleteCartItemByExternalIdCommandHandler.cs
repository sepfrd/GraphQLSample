using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class
    DeleteCartItemByExternalIdCommandHandler : IRequestHandler<DeleteCartItemByExternalIdCommand, CommandResult>
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public DeleteCartItemByExternalIdCommandHandler(
        IRepository<CartItem> cartItemRepository,
        IRepository<Cart> cartRepository,
        IRepository<User> userRepository,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(DeleteCartItemByExternalIdCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _cartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var cart = await _cartRepository.GetByInternalIdAsync(entity.CartId, cancellationToken);

        if (cart is null)
        {
            _logger.LogError(MessageConstants.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(CartItem),
                typeof(DeleteCartItemByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(DeleteCartItemByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(DeleteCartItemByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (cart.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        if (entity.Quantity > 1)
        {
            entity.Quantity -= 1;

            await _cartItemRepository.UpdateAsync(entity, cancellationToken);

            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        var deletedEntity = await _cartItemRepository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(CartItem),
            typeof(DeleteCartItemByExternalIdCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}