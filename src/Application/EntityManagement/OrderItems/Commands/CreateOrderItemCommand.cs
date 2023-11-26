#region

using Application.Common;
using Application.EntityManagement.OrderItems.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.OrderItems.Commands;

public record CreateOrderItemCommand(CreateOrderItemDto CreateOrderItemDto, int OrderExternalId) : IRequest<CommandResult>;