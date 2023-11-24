using Application.Common;
using Application.EntityManagement.CartItems.Dtos;
using MediatR;

namespace Application.EntityManagement.CartItems.Commands;

public record CreateCartItemCommand(CreateCartItemDto CreateCartItemDto) : IRequest<CommandResult>;