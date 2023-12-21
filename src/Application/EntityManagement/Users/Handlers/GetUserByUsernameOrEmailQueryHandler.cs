using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public sealed class GetUserByUsernameOrEmailQueryHandler(IRepository<User> userRepository) : IRequestHandler<GetUserByUsernameOrEmailQuery, QueryResponse<User>>
{
    public async Task<QueryResponse<User>> Handle(GetUserByUsernameOrEmailQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository
            .GetAllAsync(user =>
                    user.Username == request.UsernameOrEmail ||
                    user.Email == request.UsernameOrEmail,
                null,
                cancellationToken);

        var user = users.FirstOrDefault();

        if (user is not null)
        {
            return new QueryResponse<User>(
                user,
                true,
                MessageConstants.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        return new QueryResponse<User>(
            null,
            true,
            MessageConstants.NotFound,
            HttpStatusCode.NoContent);
    }
}