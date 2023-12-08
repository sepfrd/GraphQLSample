using Application.Common;
using Application.EntityManagement.Users.Commands;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Events;
using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users;

public class UserService
{
    private readonly IMediator _mediator;

    public UserService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination();

        var usersQuery = new GetAllUsersQuery(pagination, user => user.ExternalId == externalId);

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

    public async Task<QueryReferenceResponse<User>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var getUserQuery = new GetUserByUsernameOrEmailQuery(dto.UsernameOrEmail);

        var queryResponse = await _mediator.Send(getUserQuery, cancellationToken);

        if (queryResponse.Data is null)
        {
            return new QueryReferenceResponse<User>(
                null,
                false,
                MessageConstants.InvalidCredentials);
        }

        var user = queryResponse.Data;

        if (user.Password != dto.Password)
        {
            return new QueryReferenceResponse<User>(
                null,
                false,
                MessageConstants.InvalidCredentials);
        }

        return new QueryReferenceResponse<User>(
            user,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}