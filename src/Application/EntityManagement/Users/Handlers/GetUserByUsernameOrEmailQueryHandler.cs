using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByUsernameOrEmailQueryHandler : IRequestHandler<GetUserByUsernameOrEmailQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByUsernameOrEmailQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<QueryResponse> Handle(GetUserByUsernameOrEmailQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UserRepository
            .GetAllAsync(user =>
                    user.Username == request.UsernameOrEmail ||
                    user.Email == request.UsernameOrEmail,
                new Func<User, object?>[]
                {
                    user => user.Roles,
                    user => user.Person
                },
                cancellationToken);

        var user = users.FirstOrDefault();
        
        if (user is not null)
        {
            return new QueryResponse
                (
                user,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }
        
        return new QueryResponse
            (
            null,
            true,
            Messages.NotFound,
            HttpStatusCode.NoContent
            );
    }
}