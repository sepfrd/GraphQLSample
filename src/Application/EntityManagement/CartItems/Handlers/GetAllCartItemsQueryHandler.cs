using Application.Common;
using Application.EntityManagement.CartItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.CartItems.Handlers;

public class GetAllCartItemsQueryHandler(IRepository<CartItem> repository)
    : IRequestHandler<GetAllCartItemsQuery, QueryReferenceResponse<IEnumerable<CartItem>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<CartItem>>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<CartItem>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}