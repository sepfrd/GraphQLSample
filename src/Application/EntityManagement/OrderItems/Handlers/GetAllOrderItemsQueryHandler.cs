using System.Net;
using Application.Common;
using Application.EntityManagement.OrderItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.OrderItems.Handlers;

public class GetAllOrderItemsQueryHandler(IRepository<OrderItem> repository)
    : IRequestHandler<GetAllOrderItemsQuery, QueryReferenceResponse<IEnumerable<OrderItem>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<OrderItem>>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<OrderItem>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}