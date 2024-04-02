using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Addresses.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Addresses.Handlers;

public sealed class GetAllAddressesQueryHandler : IRequestHandler<GetAllAddressesQuery, QueryResponse<IEnumerable<Address>>>
{
    private readonly IRepository<Address> _repository;

    public GetAllAddressesQueryHandler(IRepository<Address> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Address>>> Handle(GetAllAddressesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Address>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}