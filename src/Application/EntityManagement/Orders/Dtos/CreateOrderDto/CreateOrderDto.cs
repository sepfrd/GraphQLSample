using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.OrderItems.Dtos.CreateOrderItemDto;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Shipments.Dtos.CreateShipmentDto;

namespace Application.EntityManagement.Orders.Dtos.CreateOrderDto;

public record CreateOrderDto(
    PaymentDto PaymentDto,
    CreateShipmentDto CreateShipmentDto,
    IEnumerable<CreateOrderItemDto> CreateOrderItemDtos);