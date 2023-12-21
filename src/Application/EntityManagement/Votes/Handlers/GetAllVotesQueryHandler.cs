using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Votes.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Votes.Handlers;

public sealed class GetAllVotesQueryHandler(IRepository<Vote> repository)
    : IRequestHandler<GetAllVotesQuery, QueryResponse<IEnumerable<Vote>>>
{
    public async Task<QueryResponse<IEnumerable<Vote>>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Vote>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}