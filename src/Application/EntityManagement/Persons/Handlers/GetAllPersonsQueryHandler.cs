using Application.Common;
using Application.EntityManagement.Persons.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Persons.Handlers;

public sealed class GetAllPersonsQueryHandler(IRepository<Person> repository)
    : IRequestHandler<GetAllPersonsQuery, QueryReferenceResponse<IEnumerable<Person>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Person>>> Handle(GetAllPersonsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Person>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}