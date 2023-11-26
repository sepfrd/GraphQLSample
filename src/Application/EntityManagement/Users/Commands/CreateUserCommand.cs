#region

using Application.Common;
using Application.EntityManagement.Users.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Users.Commands;

public sealed record CreateUserCommand(CreateUserDto UserDto) : IRequest<CommandResult>;