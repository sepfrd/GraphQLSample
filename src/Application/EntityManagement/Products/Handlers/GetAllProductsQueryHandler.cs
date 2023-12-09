using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Products.Handlers;

public sealed class GetAllProductsQueryHandler(IRepository<Product> repository)
    : IRequestHandler<GetAllProductsQuery, QueryReferenceResponse<IEnumerable<Product>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Product>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}