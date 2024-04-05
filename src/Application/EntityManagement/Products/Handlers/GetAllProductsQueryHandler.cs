using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Products.Handlers;

public sealed class
    GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, QueryResponse<IEnumerable<Product>>>
{
    private readonly IRepository<Product> _repository;

    public GetAllProductsQueryHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Product>>> Handle(GetAllProductsQuery request,
        CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, cancellationToken);

        return new QueryResponse<IEnumerable<Product>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}