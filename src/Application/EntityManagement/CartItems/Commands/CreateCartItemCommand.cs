using Application.Common;
using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.CartItems.Dtos.CartItemDto;
using MediatR;

namespace Application.EntityManagement.CartItems.Commands;

public record CreateCartItemCommand(CartItemDto CartItemDto) : IRequest<CommandResult>;