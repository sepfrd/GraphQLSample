using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByInternalIdQueryHandler : IRequestHandler<GetUserByInternalIdQuery, QueryReferenceResponse<User>>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByInternalIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<QueryReferenceResponse<User>> Handle(GetUserByInternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByInternalIdAsync(request.InternalId, cancellationToken, request.RelationsToInclude);

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