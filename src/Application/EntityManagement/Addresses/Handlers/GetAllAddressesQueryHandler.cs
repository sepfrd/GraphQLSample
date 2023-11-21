using System.Net;
using Application.Common;
using Application.EntityManagement.Addresses.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Addresses.Handlers;

public class GetAllAddressesQueryHandler(IRepository<Address> repository)
    : IRequestHandler<GetAllAddressesQuery, QueryReferenceResponse<IEnumerable<Address>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Address>>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Address>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}