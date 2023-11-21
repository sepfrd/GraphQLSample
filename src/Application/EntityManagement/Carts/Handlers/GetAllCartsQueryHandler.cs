using System.Net;
using Application.Common;
using Application.EntityManagement.Carts.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Carts.Handlers;

public class GetAllCartsQueryHandler(IRepository<Cart> repository)
    : IRequestHandler<GetAllCartsQuery, QueryReferenceResponse<IEnumerable<Cart>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Cart>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Cart>>
        (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
        );
    }
}