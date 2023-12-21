using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Products.Handlers;

public sealed class GetAllProductsQueryHandler(IRepository<Product> repository)
    : IRequestHandler<GetAllProductsQuery, QueryResponse<IEnumerable<Product>>>
{
    public async Task<QueryResponse<IEnumerable<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Product>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}