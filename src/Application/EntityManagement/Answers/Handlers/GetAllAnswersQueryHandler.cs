using System.Net;
using Application.Common;
using Application.EntityManagement.Answers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Answers.Handlers;

public class GetAllAnswersQueryHandler(IRepository<Answer> repository)
    : IRequestHandler<GetAllAnswersQuery, QueryReferenceResponse<IEnumerable<Answer>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Answer>>> Handle(GetAllAnswersQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Answer>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}