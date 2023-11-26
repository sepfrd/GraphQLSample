#region

using Application.Common;
using Application.EntityManagement.Carts.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Carts.Handlers;

public sealed class GetAllCartsQueryHandler : IRequestHandler<GetAllCartsQuery, QueryReferenceResponse<IEnumerable<Cart>>>
{
    private readonly IRepository<Cart> _repository;

    public GetAllCartsQueryHandler(IRepository<Cart> repository)
    {
        _repository = repository;
    }

    public async Task<QueryReferenceResponse<IEnumerable<Cart>>> Handle(GetAllCartsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Cart>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}