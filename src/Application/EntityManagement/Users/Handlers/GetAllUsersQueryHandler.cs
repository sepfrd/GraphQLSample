#region

using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Users.Handlers;

public sealed class GetAllUsersQueryHandler
    : IRequestHandler<GetAllUsersQuery, QueryReferenceResponse<IEnumerable<User>>>
{
    private readonly IRepository<User> _userRepository;

    public GetAllUsersQueryHandler(IRepository<User> userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _userRepository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<User>>(
            users,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}