using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetAllUsersQueryHandler(IRepository<User> userRepository)
    : IRequestHandler<GetAllUsersQuery, QueryReferenceResponse<IEnumerable<User>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await userRepository.GetAllAsync(request.Filter, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<User>>(
            users.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}