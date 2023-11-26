#region

using Application.Common;
using Application.EntityManagement.Users.Dtos;
using MediatR;

#endregion

namespace Application.EntityManagement.Users.Commands;

public sealed record UpdateUserCommand(UserDto UserDto) : IRequest<CommandResult>;