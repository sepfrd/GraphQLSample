using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Carts.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Carts.Handlers;

public sealed class GetAllCartsQueryHandler : IRequestHandler<GetAllCartsQuery, QueryResponse<IEnumerable<Cart>>>
{
    private readonly IRepository<Cart> _repository;

    public GetAllCartsQueryHandler(IRepository<Cart> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Cart>>> Handle(GetAllCartsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, cancellationToken);

        return new QueryResponse<IEnumerable<Cart>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}