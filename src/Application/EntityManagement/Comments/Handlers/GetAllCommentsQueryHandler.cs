using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Comments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Comments.Handlers;

public sealed class
    GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, QueryResponse<IEnumerable<Comment>>>
{
    private readonly IRepository<Comment> _repository;

    public GetAllCommentsQueryHandler(IRepository<Comment> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Comment>>> Handle(GetAllCommentsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, cancellationToken);

        return new QueryResponse<IEnumerable<Comment>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}