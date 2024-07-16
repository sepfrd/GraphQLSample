using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Persons.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Persons.Handlers;

public sealed class GetAllPersonsQueryHandler : IRequestHandler<GetAllPersonsQuery, QueryResponse<IEnumerable<Person>>>
{
    private readonly IRepository<Person> _repository;

    public GetAllPersonsQueryHandler(IRepository<Person> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Person>>> Handle(GetAllPersonsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, cancellationToken);

        return new QueryResponse<IEnumerable<Person>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}