using Application.Common;
using Application.EntityManagement.Answers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Answers.Handlers;

public sealed class GetAllAnswersQueryHandler(IRepository<Answer> repository)
    : IRequestHandler<GetAllAnswersQuery, QueryReferenceResponse<IEnumerable<Answer>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Answer>>> Handle(GetAllAnswersQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Answer>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}