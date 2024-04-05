using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.CartItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class UpdateCartItemQuantityCommandHandler : IRequestHandler<UpdateCartItemQuantityCommand, CommandResult>
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public UpdateCartItemQuantityCommandHandler(
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

    public async Task<CommandResult> Handle(UpdateCartItemQuantityCommand request, CancellationToken cancellationToken)
    {
        var cartItem = await _cartItemRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (cartItem is null || cartItem.Quantity == request.NewQuantity)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var cart = await _cartRepository.GetByInternalIdAsync(cartItem.CartId, cancellationToken);

        if (cart is null)
        {
            _logger.LogError(MessageConstants.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(CartItem),
                typeof(UpdateCartItemQuantityCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(UpdateCartItemQuantityCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(UpdateCartItemQuantityCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (cart.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        cartItem.Quantity = request.NewQuantity;

        var newCartItem = await _cartItemRepository.UpdateAsync(cartItem, cancellationToken);

        if (newCartItem is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(CartItem),
            typeof(UpdateCartItemQuantityCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}