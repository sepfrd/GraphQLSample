using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Shipments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Shipments.Handlers;

public sealed class GetAllShipmentsQueryHandler(IRepository<Shipment> repository)
    : IRequestHandler<GetAllShipmentsQuery, QueryResponse<IEnumerable<Shipment>>>
{
    public async Task<QueryResponse<IEnumerable<Shipment>>> Handle(GetAllShipmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Shipment>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}