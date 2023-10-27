using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<QueryResponse> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork
            .UserRepository
            .GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryResponse
            (
            users.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}