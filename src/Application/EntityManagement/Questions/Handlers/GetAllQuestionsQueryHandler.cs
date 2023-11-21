using System.Net;
using Application.Common;
using Application.EntityManagement.Questions.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Questions.Handlers;

public class GetAllQuestionsQueryHandler(IRepository<Question> repository)
    : IRequestHandler<GetAllQuestionsQuery, QueryReferenceResponse<IEnumerable<Question>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Question>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Question>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}