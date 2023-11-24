using Application.Common;
using Application.EntityManagement.OrderItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.OrderItems.Handlers;

public sealed class GetAllOrderItemsQueryHandler(IRepository<OrderItem> repository)
    : IRequestHandler<GetAllOrderItemsQuery, QueryReferenceResponse<IEnumerable<OrderItem>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<OrderItem>>> Handle(GetAllOrderItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<OrderItem>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}