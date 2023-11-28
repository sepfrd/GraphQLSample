using Application.Common;
using Application.EntityManagement.Roles.Commands;
using Application.EntityManagement.Roles.Events;
using Application.EntityManagement.Roles.Queries;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Roles;

public class RoleService
{
    private readonly IMediator _mediator;

    public RoleService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination();

        var rolesQuery = new GetAllRolesQuery(pagination, role => role.ExternalId == externalId);

        var roleResult = await _mediator.Send(rolesQuery, cancellationToken);

        if (!roleResult.IsSuccessful ||
            roleResult.Data is null ||
            !roleResult.Data.Any())
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deleteRoleCommand = new DeleteRoleByExternalIdCommand(externalId);

        await _mediator.Send(deleteRoleCommand, cancellationToken);

        var roleDeletedEvent = new RoleDeletedEvent(roleResult.Data.First());

        await _mediator.Publish(roleDeletedEvent, cancellationToken);

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}