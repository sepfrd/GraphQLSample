using Application.Common;
using Application.EntityManagement.Users.Queries;
using Domain.Abstractions;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Users.Handlers;

public class GetUserByInternalIdQueryHandler : IRequestHandler<GetUserByInternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;

    public GetUserByInternalIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<QueryResponse> Handle(GetUserByInternalIdQuery request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByInternalIdAsync(request.InternalId, request.RelationsToInclude, cancellationToken);

        if (user is null)
        {
            return new QueryResponse
                (
                null,
                true,
                Messages.NotFound,
                HttpStatusCode.NoContent
                );
        }

        return new QueryResponse
            (
            user,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}