using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.Orders.Commands;
using Application.EntityManagement.Orders.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Shipments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Orders.Handlers;

public class CreateOrderCommandHandler : IRequestHandler<CreateOrderCommand, CommandResult>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<Shipment> _shipmentRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Address> _addressRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateOrderCommandHandler(
        IRepository<Order> orderRepository,
        IRepository<User> userRepository,
        IRepository<OrderItem> orderItemRepository,
        IRepository<Payment> paymentRepository,
        IRepository<Shipment> shipmentRepository,
        IRepository<Product> productRepository,
        IRepository<Address> addressRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _orderRepository = orderRepository;
        _mappingService = mappingService;
        _logger = logger;
        _userRepository = userRepository;
        _orderItemRepository = orderItemRepository;
        _paymentRepository = paymentRepository;
        _shipmentRepository = shipmentRepository;
        _productRepository = productRepository;
        _addressRepository = addressRepository;
    }

    public async Task<CommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.CreateOrderDto.UserExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var orderId = Guid.NewGuid();

        var orderItemsResult = await CreateOrderItemsAsync(request.CreateOrderDto.CreateOrderItemDtos, orderId, cancellationToken);

        if (orderItemsResult.HttpStatusCode != HttpStatusCode.OK)
        {
            return orderItemsResult.HttpStatusCode switch
            {
                HttpStatusCode.BadRequest => CommandResult.Failure(Messages.BadRequest),
                HttpStatusCode.InternalServerError => CommandResult.Failure(Messages.InternalServerError),
                _ => CommandResult.Failure(Messages.BadRequest)
            };
        }

        var orderItems = (List<OrderItem>)orderItemsResult.Data!;

        var paymentResult = await CreatePaymentAsync(request.CreateOrderDto.CreatePaymentDto, orderId, cancellationToken);

        if (paymentResult.HttpStatusCode != HttpStatusCode.OK)
        {
            foreach (var orderItem in orderItems)
            {
                await _orderItemRepository.DeleteAsync(orderItem, cancellationToken);
            }

            return paymentResult.HttpStatusCode switch
            {
                HttpStatusCode.BadRequest => CommandResult.Failure(Messages.BadRequest),
                HttpStatusCode.InternalServerError => CommandResult.Failure(Messages.InternalServerError),
                _ => CommandResult.Failure(Messages.BadRequest)
            };
        }

        var payment = (Payment)paymentResult.Data!;

        var shipmentResult = await CreateShipmentAsync(request.CreateOrderDto.CreateShipmentDto, orderId, cancellationToken);

        if (shipmentResult.HttpStatusCode != HttpStatusCode.OK)
        {
            foreach (var orderItem in orderItems)
            {
                await _orderItemRepository.DeleteAsync(orderItem, cancellationToken);
            }

            await _paymentRepository.DeleteAsync(payment, cancellationToken);

            return shipmentResult.HttpStatusCode switch
            {
                HttpStatusCode.BadRequest => CommandResult.Failure(Messages.BadRequest),
                HttpStatusCode.InternalServerError => CommandResult.Failure(Messages.InternalServerError),
                _ => CommandResult.Failure(Messages.BadRequest)
            };
        }

        var shipment = (Shipment)shipmentResult.Data!;

        var order = new Order
        {
            InternalId = orderId,
            UserId = user.InternalId,
            PaymentId = payment.InternalId,
            ShipmentId = shipment.InternalId,
        };

        var createdEntity = await _orderRepository.CreateAsync(order, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Order), typeof(CreateOrderCommandHandler));

        foreach (var orderItem in orderItems)
        {
            await _orderItemRepository.DeleteAsync(orderItem, cancellationToken);
        }

        await _paymentRepository.DeleteAsync(payment, cancellationToken);

        await _shipmentRepository.DeleteAsync(shipment, cancellationToken);

        return CommandResult.Failure(Messages.InternalServerError);
    }

    private async Task<OrderCreationResultDto> CreateOrderItemsAsync(IEnumerable<CreateOrderItemDto> createOrderItemDtos, Guid orderId, CancellationToken cancellationToken = default)
    {
        var orderItems = new List<OrderItem>();

        foreach (var orderItemDto in createOrderItemDtos)
        {
            var product = await _productRepository.GetByExternalIdAsync(orderItemDto.ProductExternalId, cancellationToken);

            if (product is null)
            {
                return new OrderCreationResultDto(null, HttpStatusCode.BadRequest);
            }

            var orderItemEntity = _mappingService.Map<CreateOrderItemDto, OrderItem>(orderItemDto);

            if (orderItemEntity is null)
            {
                _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(OrderItem), typeof(CreateOrderCommandHandler));

                return new OrderCreationResultDto(null, HttpStatusCode.InternalServerError);
            }

            orderItemEntity.OrderId = orderId;
            orderItemEntity.ProductId = product.InternalId;

            var createdOrderItemEntity = await _orderItemRepository.CreateAsync(orderItemEntity, cancellationToken);

            if (createdOrderItemEntity is null)
            {
                _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(OrderItem), typeof(CreateOrderCommandHandler));

                foreach (var item in orderItems)
                {
                    await _orderItemRepository.DeleteAsync(item, cancellationToken);
                }

                return new OrderCreationResultDto(null, HttpStatusCode.InternalServerError);
            }

            orderItems.Add(createdOrderItemEntity);
        }

        return new OrderCreationResultDto(orderItems, HttpStatusCode.OK);
    }

    private async Task<OrderCreationResultDto> CreatePaymentAsync(CreatePaymentDto createPaymentDto, Guid orderId, CancellationToken cancellationToken = default)
    {
        var paymentEntity = _mappingService.Map<CreatePaymentDto, Payment>(createPaymentDto);

        if (paymentEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Payment), typeof(CreateOrderCommandHandler));

            return new OrderCreationResultDto(null, HttpStatusCode.InternalServerError);
        }

        paymentEntity.OrderId = orderId;

        var createdPaymentEntity = await _paymentRepository.CreateAsync(paymentEntity, cancellationToken);

        if (createdPaymentEntity is not null)
        {
            return new OrderCreationResultDto(createdPaymentEntity, HttpStatusCode.OK);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Payment), typeof(CreateOrderCommandHandler));

        return new OrderCreationResultDto(null, HttpStatusCode.InternalServerError);
    }

    private async Task<OrderCreationResultDto> CreateShipmentAsync(CreateShipmentDto createShipmentDto, Guid orderId, CancellationToken cancellationToken = default)
    {
        var originAddress = await _addressRepository.GetByExternalIdAsync(createShipmentDto.OriginAddressExternalId, cancellationToken);

        if (originAddress is null)
        {
            return new OrderCreationResultDto(null, HttpStatusCode.BadRequest);
        }

        var destinationAddress = await _addressRepository.GetByExternalIdAsync(createShipmentDto.DestinationAddressExternalId, cancellationToken);

        if (destinationAddress is null)
        {
            return new OrderCreationResultDto(null, HttpStatusCode.BadRequest);
        }

        var shipmentEntity = _mappingService.Map<CreateShipmentDto, Shipment>(createShipmentDto);

        if (shipmentEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Shipment), typeof(CreateOrderCommandHandler));

            return new OrderCreationResultDto(null, HttpStatusCode.InternalServerError);
        }

        shipmentEntity.OrderId = orderId;
        shipmentEntity.OriginAddressId = originAddress.InternalId;
        shipmentEntity.DestinationAddressId = destinationAddress.InternalId;

        var createdShipmentEntity = await _shipmentRepository.CreateAsync(shipmentEntity, cancellationToken);

        if (createdShipmentEntity is not null)
        {
            return new OrderCreationResultDto(createdShipmentEntity, HttpStatusCode.OK);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Shipment), typeof(CreateOrderCommandHandler));

        return new OrderCreationResultDto(null, HttpStatusCode.InternalServerError);
    }
}