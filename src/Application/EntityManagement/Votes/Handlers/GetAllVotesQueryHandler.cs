using Application.Common;
using Application.EntityManagement.Votes.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Votes.Handlers;

public class GetAllVotesQueryHandler(IRepository<Vote> repository)
    : IRequestHandler<GetAllVotesQuery, QueryReferenceResponse<IEnumerable<Vote>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Vote>>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<Vote>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}