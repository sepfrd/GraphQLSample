using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.CartItems.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.CartItems.Handlers;

public class GetAllCartItemsQueryHandler : IRequestHandler<GetAllCartItemsQuery, QueryResponse<IEnumerable<CartItem>>>
{
    private readonly IRepository<CartItem> _repository;

    public GetAllCartItemsQueryHandler(IRepository<CartItem> repository)
    {
        _repository = repository;
    }

    public virtual async Task<QueryResponse<IEnumerable<CartItem>>> Handle(GetAllCartItemsQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<CartItem>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}