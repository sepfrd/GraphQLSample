using Application.Common;
using Application.EntityManagement.Products.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Products.Handlers;

public class GetAllProductsQueryHandler(IRepository<Product> repository)
    : IRequestHandler<GetAllProductsQuery, QueryReferenceResponse<IEnumerable<Product>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Product>>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<Product>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}