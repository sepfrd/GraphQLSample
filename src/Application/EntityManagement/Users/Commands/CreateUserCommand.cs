using Application.Common;
using Application.EntityManagement.Users.Dtos;
using MediatR;

namespace Application.EntityManagement.Users.Commands.CreateUser;

public record CreateUserCommand(CreateUserDto UserDto) : IRequest<CommandResult>;