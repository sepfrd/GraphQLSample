using Application.Common;
using Application.EntityManagement.Addresses.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Addresses.Handlers;

public sealed class GetAllAddressesQueryHandler(IRepository<Address> repository)
    : IRequestHandler<GetAllAddressesQuery, QueryReferenceResponse<IEnumerable<Address>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Address>>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Address>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}