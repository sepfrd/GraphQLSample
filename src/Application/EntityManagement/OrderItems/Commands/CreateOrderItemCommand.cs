using Application.Common.Commands;
using Application.EntityManagement.OrderItems.Dtos;

namespace Application.EntityManagement.OrderItems.Commands;

public record CreateOrderItemCommand(OrderItemDto OrderItemDto) : BaseCreateCommand<OrderItemDto>(OrderItemDto);