using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.OrderItems.Commands;
using Application.EntityManagement.OrderItems.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class CreateOrderItemCommandHandler : IRequestHandler<CreateOrderItemCommand, CommandResult>
{
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateOrderItemCommandHandler(
        IRepository<OrderItem> orderItemRepository,
        IRepository<Order> orderRepository,
        IRepository<Product> productRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _orderItemRepository = orderItemRepository;
        _orderRepository = orderRepository;
        _productRepository = productRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var order = await _orderRepository.GetByExternalIdAsync(request.OrderExternalId, cancellationToken);

        if (order is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var product = await _productRepository.GetByExternalIdAsync(request.CreateOrderItemDto.ProductExternalId, cancellationToken);

        if (product is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var entity = _mappingService.Map<CreateOrderItemDto, OrderItem>(request.CreateOrderItemDto);

        if (entity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(OrderItem), typeof(CreateOrderItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        entity.OrderId = order.InternalId;
        entity.ProductId = product.InternalId;

        var createdEntity = await _orderItemRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(OrderItem), typeof(CreateOrderItemCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}