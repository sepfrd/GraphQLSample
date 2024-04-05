using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Roles.Commands;
using Application.EntityManagement.Roles.Events;
using Application.EntityManagement.Roles.Queries;
using MediatR;

namespace Application.EntityManagement.Roles;

public class RoleService
{
    private readonly IMediator _mediator;

    public RoleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId,
        CancellationToken cancellationToken = default)
    {
        var rolesQuery = new GetAllRolesQuery(role => role.ExternalId == externalId);

        var roleResult = await _mediator.Send(rolesQuery, cancellationToken);

        if (!roleResult.IsSuccessful ||
            roleResult.Data is null ||
            !roleResult.Data.Any())
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deleteRoleCommand = new DeleteRoleByExternalIdCommand(externalId);

        await _mediator.Send(deleteRoleCommand, cancellationToken);

        var roleDeletedEvent = new RoleDeletedEvent(roleResult.Data.First());

        await _mediator.Publish(roleDeletedEvent, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
    }
}