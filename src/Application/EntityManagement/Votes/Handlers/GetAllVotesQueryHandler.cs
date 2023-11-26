using Application.Common;
using Application.EntityManagement.Votes.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Votes.Handlers;

public sealed class GetAllVotesQueryHandler(IRepository<Vote> repository)
    : IRequestHandler<GetAllVotesQuery, QueryReferenceResponse<IEnumerable<Vote>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Vote>>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Vote>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}