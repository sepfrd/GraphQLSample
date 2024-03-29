using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Votes.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Votes.Handlers;

public sealed class GetAllVotesQueryHandler : IRequestHandler<GetAllVotesQuery, QueryResponse<IEnumerable<Vote>>>
{
    private readonly IRepository<Vote> _repository;

    public GetAllVotesQueryHandler(IRepository<Vote> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Vote>>> Handle(GetAllVotesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Vote>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}