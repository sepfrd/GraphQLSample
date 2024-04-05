using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Orders.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Orders.Handlers;

public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, QueryResponse<IEnumerable<Order>>>
{
    private readonly IRepository<Order> _repository;

    public GetAllOrdersQueryHandler(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Order>>> Handle(GetAllOrdersQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, cancellationToken);

        return new QueryResponse<IEnumerable<Order>>
        (
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}