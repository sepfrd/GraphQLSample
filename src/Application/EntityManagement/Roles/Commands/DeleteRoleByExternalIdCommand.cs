using Application.Common;
using MediatR;

namespace Application.EntityManagement.Roles.Commands;

public record DeleteRoleByExternalIdCommand(int ExternalId) : IRequest<CommandResult>;