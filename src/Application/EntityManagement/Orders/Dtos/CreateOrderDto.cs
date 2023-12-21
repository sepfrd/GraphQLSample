using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Shipments.Dtos;

namespace Application.EntityManagement.Orders.Dtos;

public record CreateOrderDto(
    CreatePaymentDto CreatePaymentDto,
    CreateShipmentDto CreateShipmentDto,
    IEnumerable<CreateOrderItemDto> CreateOrderItemDtos);