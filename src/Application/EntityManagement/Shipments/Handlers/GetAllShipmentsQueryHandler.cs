using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Shipments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Shipments.Handlers;

public sealed class GetAllShipmentsQueryHandler : IRequestHandler<GetAllShipmentsQuery, QueryResponse<IEnumerable<Shipment>>>
{
    private readonly IRepository<Shipment> _repository;

    public GetAllShipmentsQueryHandler(IRepository<Shipment> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Shipment>>> Handle(GetAllShipmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Shipment>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}