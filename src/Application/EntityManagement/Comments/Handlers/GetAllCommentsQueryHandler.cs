using Application.Common;
using Application.EntityManagement.Comments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Comments.Handlers;

public sealed class GetAllCommentsQueryHandler : IRequestHandler<GetAllCommentsQuery, QueryReferenceResponse<IEnumerable<Comment>>>
{
    private readonly IRepository<Comment> _repository;

    public GetAllCommentsQueryHandler(IRepository<Comment> repository)
    {
        _repository = repository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<Comment>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Comment>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}