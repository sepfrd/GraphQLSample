#region

using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Users.Handlers;

public sealed class GetUserByUsernameOrEmailQueryHandler(IRepository<User> userRepository) : IRequestHandler<GetUserByUsernameOrEmailQuery, QueryReferenceResponse<User>>
{
    public async Task<QueryReferenceResponse<User>> Handle(GetUserByUsernameOrEmailQuery request, CancellationToken cancellationToken)
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
            return new QueryReferenceResponse<User>(
                user,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        return new QueryReferenceResponse<User>(
            null,
            true,
            Messages.NotFound,
            HttpStatusCode.NoContent);
    }
}