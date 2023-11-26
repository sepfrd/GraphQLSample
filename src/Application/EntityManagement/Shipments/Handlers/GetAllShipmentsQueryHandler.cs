#region

using Application.Common;
using Application.EntityManagement.Shipments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Shipments.Handlers;

public sealed class GetAllShipmentsQueryHandler(IRepository<Shipment> repository)
    : IRequestHandler<GetAllShipmentsQuery, QueryReferenceResponse<IEnumerable<Shipment>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Shipment>>> Handle(GetAllShipmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Shipment>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}