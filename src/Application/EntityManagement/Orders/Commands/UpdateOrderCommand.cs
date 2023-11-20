using Application.Common;
using Application.EntityManagement.Orders.Dtos;
using MediatR;

namespace Application.EntityManagement.Orders.Commands;

public record UpdateOrderCommand(int ExternalId, OrderDto OrderDto) : IRequest<CommandResult>;