using Application.Common;
using Application.EntityManagement.Users.Dtos;
using MediatR;

namespace Application.EntityManagement.Users.Commands;

public record LoginCommand(LoginDto LoginDto) : IRequest<CommandResult>;