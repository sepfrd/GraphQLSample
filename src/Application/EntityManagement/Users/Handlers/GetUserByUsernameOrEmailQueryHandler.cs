using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Handlers;

public sealed class
    GetUserByUsernameOrEmailQueryHandler : IRequestHandler<GetUserByUsernameOrEmailQuery, QueryResponse<User>>
{
    private readonly IRepository<User> _userRepository;

    public GetUserByUsernameOrEmailQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<QueryResponse<User>> Handle(GetUserByUsernameOrEmailQuery request,
        CancellationToken cancellationToken)
    {
        var users = await _userRepository
            .GetAllAsync(user =>
                    user.Username == request.UsernameOrEmail ||
                    user.Email == request.UsernameOrEmail,
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