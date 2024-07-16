using Application.Common;
using Application.EntityManagement.OrderItems.Dtos.CreateOrderItemDto;
using MediatR;

namespace Application.EntityManagement.OrderItems.Commands;

public record CreateOrderItemCommand(CreateOrderItemDto CreateOrderItemDto, int OrderExternalId)
    : IRequest<CommandResult>;