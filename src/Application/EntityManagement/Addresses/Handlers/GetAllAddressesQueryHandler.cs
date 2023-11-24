using Application.Common;
using Application.EntityManagement.Addresses.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Addresses.Handlers;

public sealed class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, QueryReferenceResponse<IEnumerable<Address>>>
{
    private readonly IRepository<Address> _repository;

    public GetAllAddressesQueryHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<Address>>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Address>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}