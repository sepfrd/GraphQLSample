using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Questions.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Questions.Handlers;

public sealed class GetAllQuestionsQueryHandler(IRepository<Question> repository)
    : IRequestHandler<GetAllQuestionsQuery, QueryResponse<IEnumerable<Question>>>
{
    public async Task<QueryResponse<IEnumerable<Question>>> Handle(GetAllQuestionsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Question>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}