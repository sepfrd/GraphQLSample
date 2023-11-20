using Application.Common;
using Application.EntityManagement.OrderItems.Dtos;
using MediatR;

namespace Application.EntityManagement.OrderItems.Commands;

public record CreateOrderItemCommand(OrderItemDto OrderItemDto) : IRequest<CommandResult>;