using Application.Common;
using Application.EntityManagement.Comments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Comments.Handlers;

public class GetAllCommentsQueryHandler(IRepository<Comment> repository)
    : IRequestHandler<GetAllCommentsQuery, QueryReferenceResponse<IEnumerable<Comment>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Comment>>> Handle(GetAllCommentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<Comment>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}