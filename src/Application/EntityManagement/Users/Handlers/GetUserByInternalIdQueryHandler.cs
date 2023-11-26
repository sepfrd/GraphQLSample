#region

using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByInternalIdQueryHandler(IRepository<User> userRepository) : IRequestHandler<GetUserByInternalIdQuery, QueryReferenceResponse<User>>
{
    public async Task<QueryReferenceResponse<User>> Handle(GetUserByInternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (user is null)
        {
            return new QueryReferenceResponse<User>(
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NoContent);
        }

        return new QueryReferenceResponse<User>(
            user,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}