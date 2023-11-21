using System.Net;
using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users;

public class UserService
{
    private readonly ISender _sender;

    public UserService(ISender sender)
    {
        _sender = sender;
    }

    public async Task<QueryReferenceResponse<User>> LoginAsync(LoginDto dto, CancellationToken cancellationToken = default)
    {
        var getUserQuery = new GetUserByUsernameOrEmailQuery(dto.UsernameOrEmail);

        var queryResponse = await _sender.Send(getUserQuery, cancellationToken);

        if (queryResponse.Data is null)
        {
            return new QueryReferenceResponse<User>(
                null,
                false,
                Messages.InvalidCredentials);
        }

        var user = queryResponse.Data;

        if (user.Password != dto.Password)
        {
            return new QueryReferenceResponse<User>(
                null,
                false,
                Messages.InvalidCredentials);
        }

        return new QueryReferenceResponse<User>(
            user,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}