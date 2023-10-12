using Application.Common;
using MediatR;

namespace Application.EntityManagement.Users.Commands;

public sealed record DeleteUserByInternalIdCommand(Guid InternalId) : IRequest<CommandResult>;