using Application.Common;
using Application.EntityManagement.Users.Dtos.CreateUserDto;
using MediatR;

namespace Application.EntityManagement.Users.Commands;

public sealed record CreateUserCommand(CreateUserDto UserDto) : IRequest<CommandResult>;