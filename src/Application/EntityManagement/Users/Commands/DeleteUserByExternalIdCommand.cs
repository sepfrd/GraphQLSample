using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Commands;

public sealed record DeleteUserByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;