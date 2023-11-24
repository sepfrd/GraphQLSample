using Application.Common;
using Application.EntityManagement.Comments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Comments.Handlers;

public sealed class GetAllCommentsQueryHandler(IRepository<Comment> repository)
    : IRequestHandler<GetAllCommentsQuery, QueryReferenceResponse<IEnumerable<Comment>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Comment>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Comment>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}