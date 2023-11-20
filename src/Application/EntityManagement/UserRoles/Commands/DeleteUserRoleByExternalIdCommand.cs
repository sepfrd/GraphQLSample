using Application.Common;
using MediatR;

namespace Application.EntityManagement.UserRoles.Commands;

public record DeleteUserRoleByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;