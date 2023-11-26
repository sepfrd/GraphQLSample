#region

using Application.Common;
using Application.EntityManagement.Answers.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Answers.Handlers;

public sealed class GetAllAnswersQueryHandler : IRequestHandler<GetAllAnswersQuery, QueryReferenceResponse<IEnumerable<Answer>>>
{
    private readonly IRepository<Answer> _repository;

    public GetAllAnswersQueryHandler(IRepository<Answer> repository)
    {
        _repository = repository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<Answer>>> Handle(GetAllAnswersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Answer>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}