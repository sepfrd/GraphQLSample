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
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateCartItemCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateCartItemCommand request, CancellationToken cancellationToken)
    {
        var cartEntity = await _unitOfWork.CartRepository.GetByExternalIdAsync(request.CartItemDto.CartExternalId, new Func<Cart, object?>[]
            {
                cart => cart.CartItems
            },
            cancellationToken);

        if (cartEntity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        if (cartEntity.CartItems is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Cart), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var existingCartItem = cartEntity.CartItems.FirstOrDefault(item => item.Product?.ExternalId == request.CartItemDto.ProductDto.ExternalId);

        if (existingCartItem is not null)
        {
            existingCartItem.Quantity += request.CartItemDto.Quantity;

            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        var cartItem = _mappingService.Map<CartItemDto, CartItem>(request.CartItemDto);

        if (cartItem is null)
        {
            _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var productEntity = await _unitOfWork.ProductRepository.GetByExternalIdAsync(request.CartItemDto.ProductDto.ExternalId, null, cancellationToken);

        if (productEntity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var externalId = await _unitOfWork.CartItemRepository.GenerateUniqueExternalIdAsync(cancellationToken);

        cartItem.CartId = cartEntity.InternalId;
        cartItem.ProductId = productEntity.InternalId;
        cartItem.ExternalId = externalId;

        var createdCartItem = await _unitOfWork.CartItemRepository.CreateAsync(cartItem, cancellationToken);

        if (createdCartItem is null)
        {
            _logger.LogError(Messages.EntityCreationFailed, DateTime.UtcNow, typeof(CartItem), typeof(CreateCartItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyCreated);
    }
}