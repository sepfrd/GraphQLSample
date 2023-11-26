#region

using Application.Common;
using Application.EntityManagement.Orders.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Orders.Commands;

public record CreateOrderCommand(CreateOrderDto CreateOrderDto) : IRequest<CommandResult>;