using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users;

public class UserService
{
    private readonly ISender _sender;

    public UserService(ISender sender)
    {
        _sender = sender;
    }

    public async Task<QueryResponse?> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var getUserQuery = new GetUserByUsernameOrEmailQuery(dto.UsernameOrEmail);

        var queryResponse = await _sender.Send(getUserQuery, cancellationToken);

        if (queryResponse.Data is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.InvalidCredentials,
                HttpStatusCode.BadRequest
                );
        }

        var user = (User)queryResponse.Data;

        if (user.Password != dto.Password)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.InvalidCredentials,
                HttpStatusCode.BadRequest
                );
        }

        return new QueryResponse
            (
            user,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}