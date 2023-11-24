using Application.Common;
using Application.EntityManagement.Orders.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Orders.Handlers;

public sealed class GetAllOrdersQueryHandler(IRepository<Order> repository)
    : IRequestHandler<GetAllOrdersQuery, QueryReferenceResponse<IEnumerable<Order>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Order>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Order>>
            (
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}