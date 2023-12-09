using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Orders.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Orders.Handlers;

public sealed class GetAllOrdersQueryHandler : IRequestHandler<GetAllOrdersQuery, QueryReferenceResponse<IEnumerable<Order>>>
{
    private readonly IRepository<Order> _repository;

    public GetAllOrdersQueryHandler(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<Order>>> Handle(GetAllOrdersQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Order>>
            (
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}