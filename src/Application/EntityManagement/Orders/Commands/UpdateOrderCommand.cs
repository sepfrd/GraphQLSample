using Application.Common.Commands;
using Application.EntityManagement.Orders.Dtos;

namespace Application.EntityManagement.Orders.Commands;

public record UpdateOrderCommand(int ExternalId, OrderDto OrderDto) : BaseUpdateCommand<OrderDto>(ExternalId, OrderDto);