using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByInternalIdQueryHandler(IRepository<User> userRepository) : IRequestHandler<GetUserByInternalIdQuery, QueryResponse<User>>
{
    public async Task<QueryResponse<User>> Handle(GetUserByInternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (user is null)
        {
            return new QueryResponse<User>(
                null,
                true,
                MessageConstants.NotFound,
                HttpStatusCode.NoContent);
        }

        return new QueryResponse<User>(
            user,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}