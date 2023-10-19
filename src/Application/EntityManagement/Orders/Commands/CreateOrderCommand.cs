using Application.Common.Commands;
using Application.EntityManagement.Orders.Dtos;

namespace Application.EntityManagement.Orders.Commands;

public record CreateOrderCommand(OrderDto OrderDto) : BaseCreateCommand<OrderDto>(OrderDto);