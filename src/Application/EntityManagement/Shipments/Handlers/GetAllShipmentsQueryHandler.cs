using System.Net;
using Application.Common;
using Application.EntityManagement.Shipments.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Shipments.Handlers;

public class GetAllShipmentsQueryHandler(IRepository<Shipment> repository)
    : IRequestHandler<GetAllShipmentsQuery, QueryReferenceResponse<IEnumerable<Shipment>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Shipment>>> Handle(GetAllShipmentsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Shipment>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}