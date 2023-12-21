using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Persons.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Persons.Handlers;

public sealed class GetAllPersonsQueryHandler(IRepository<Person> repository)
    : IRequestHandler<GetAllPersonsQuery, QueryResponse<IEnumerable<Person>>>
{
    public async Task<QueryResponse<IEnumerable<Person>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Person>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}