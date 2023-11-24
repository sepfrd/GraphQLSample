using Application.Common;
using Application.EntityManagement.Carts.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Carts.Handlers;

public sealed class GetAllCartsQueryHandler(IRepository<Cart> repository)
    : IRequestHandler<GetAllCartsQuery, QueryReferenceResponse<IEnumerable<Cart>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Cart>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Cart>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}