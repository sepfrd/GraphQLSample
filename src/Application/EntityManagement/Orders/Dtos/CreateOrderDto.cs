#region

using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Shipments.Dtos;

#endregion

namespace Application.EntityManagement.Orders.Dtos;

public record CreateOrderDto(
    int UserExternalId,
    CreatePaymentDto CreatePaymentDto,
    CreateShipmentDto CreateShipmentDto,
    IEnumerable<CreateOrderItemDto> CreateOrderItemDtos);