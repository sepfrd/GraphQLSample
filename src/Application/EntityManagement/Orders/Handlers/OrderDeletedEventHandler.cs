using Application.EntityManagement.Orders.Events;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Orders.Handlers;

public class OrderDeletedEventHandler : INotificationHandler<OrderDeletedEvent>
{
    private readonly IRepository<Payment> _paymentRepository;
    private readonly IRepository<Shipment> _shipmentRepository;
    private readonly IRepository<OrderItem> _orderItemRepository;

    public OrderDeletedEventHandler(
        IRepository<Payment> paymentRepository,
        IRepository<Shipment> shipmentRepository,
        IRepository<OrderItem> orderItemRepository)
    {
        _paymentRepository = paymentRepository;
        _shipmentRepository = shipmentRepository;
        _orderItemRepository = orderItemRepository;
    }

    public async Task Handle(OrderDeletedEvent notification, CancellationToken cancellationToken)
    {
        var pagination = new Pagination(1, int.MaxValue);

        var orderItems = (await _orderItemRepository.GetAllAsync(
                orderItem => orderItem.OrderId == notification.Entity.InternalId, pagination,
                cancellationToken))
            .ToList();

        if (orderItems.Count != 0)
        {
            await _orderItemRepository.DeleteManyAsync(orderItems, cancellationToken);
        }

        var shipment = await _shipmentRepository.GetByInternalIdAsync(notification.Entity.ShipmentId, cancellationToken);

        if (shipment is not null)
        {
            await _shipmentRepository.DeleteOneAsync(shipment, cancellationToken);
        }

        var payment = await _paymentRepository.GetByInternalIdAsync(notification.Entity.PaymentId, cancellationToken);

        if (payment is not null)
        {
            await _paymentRepository.DeleteOneAsync(payment, cancellationToken);
        }
    }
}