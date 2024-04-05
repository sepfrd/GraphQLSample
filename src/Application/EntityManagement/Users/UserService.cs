using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Events;
using Application.EntityManagement.Users.Queries;
using MediatR;

namespace Application.EntityManagement.Users;

public class UserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId,
        CancellationToken cancellationToken = default)
    {
        var usersQuery = new GetAllUsersQuery(user => user.ExternalId == externalId);

        var userResult = await _mediator.Send(usersQuery, cancellationToken);

        if (!userResult.IsSuccessful ||
            userResult.Data is null ||
            !userResult.Data.Any())
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deleteUserCommand = new DeleteUserByExternalIdCommand(externalId);

        await _mediator.Send(deleteUserCommand, cancellationToken);

        var userDeletedEvent = new UserDeletedEvent(userResult.Data.First());

        await _mediator.Publish(userDeletedEvent, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
    }
}