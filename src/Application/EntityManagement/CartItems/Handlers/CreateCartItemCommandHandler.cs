using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.CartItems.Commands;
using Application.EntityManagement.CartItems.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.CartItems.Handlers;

public class CreateCartItemCommandHandler : IRequestHandler<CreateCartItemCommand, CommandResult>
{
    private readonly IRepository<CartItem> _cartItemRepository;
    private readonly IRepository<Cart> _cartRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreateCartItemCommandHandler(IRepository<CartItem> cartItemRepository,
        IRepository<Cart> cartRepository,
        IRepository<Product> productRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartEntity = await _cartRepository.GetByExternalIdAsync(
            request.CreateCartItemDto.CartExternalId,
            cancellationToken);

        if (cartEntity is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        if (cartEntity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var product = await _productRepository.GetByExternalIdAsync(request.CreateCartItemDto.ProductExternalId, cancellationToken);

        if (product is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        if (cartEntity.CartItems is null)
        {
            _logger.LogError(MessageConstants.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var existingCartItem = cartEntity.CartItems.FirstOrDefault(item => item.Product?.ExternalId == product.ExternalId);

        if (existingCartItem is not null)
        {
            existingCartItem.Quantity += request.CreateCartItemDto.Quantity;

            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        var cartItem = _mappingService.Map<CreateCartItemDto, CartItem>(request.CreateCartItemDto);

        if (cartItem is null)
        {
            _logger.LogError(MessageConstants.MappingFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        cartItem.CartId = cartEntity.InternalId;
        cartItem.ProductId = product.InternalId;

        var createdCartItem = await _cartItemRepository.CreateAsync(cartItem, cancellationToken);

        if (createdCartItem is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}