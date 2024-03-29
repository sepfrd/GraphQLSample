using Application.Common;
using Application.EntityManagement.Orders.Dtos.CreateOrderDto;
using MediatR;

namespace Application.EntityManagement.Orders.Commands;

public record CreateOrderCommand(CreateOrderDto CreateOrderDto) : IRequest<CommandResult>;