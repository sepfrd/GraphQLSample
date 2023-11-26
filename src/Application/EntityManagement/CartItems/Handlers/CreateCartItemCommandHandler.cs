using Application.Abstractions;
using Application.Common;
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
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateCartItemCommandHandler(IRepository<CartItem> cartItemRepository,
        IRepository<Cart> cartRepository,
        IRepository<Product> productRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _cartItemRepository = cartItemRepository;
        _cartRepository = cartRepository;
        _productRepository = productRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartEntity = await _cartRepository.GetByExternalIdAsync(
            request.CreateCartItemDto.CartExternalId,
            cancellationToken);

        if (cartEntity is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var product = await _productRepository.GetByExternalIdAsync(request.CreateCartItemDto.ProductExternalId, cancellationToken);

        if (product is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        if (cartEntity.CartItems is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var existingCartItem = cartEntity.CartItems.FirstOrDefault(item => item.Product?.ExternalId == product.ExternalId);

        if (existingCartItem is not null)
        {
            existingCartItem.Quantity += request.CreateCartItemDto.Quantity;

            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        var cartItem = _mappingService.Map<CreateCartItemDto, CartItem>(request.CreateCartItemDto);

        if (cartItem is null)
        {
            _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        cartItem.CartId = cartEntity.InternalId;
        cartItem.ProductId = product.InternalId;

        var createdCartItem = await _cartItemRepository.CreateAsync(cartItem, cancellationToken);

        if (createdCartItem is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}