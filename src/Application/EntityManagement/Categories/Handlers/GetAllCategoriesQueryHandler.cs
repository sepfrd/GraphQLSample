using System.Net;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Categories.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Categories.Handlers;

public sealed class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, QueryResponse<IEnumerable<Category>>>
{
    private readonly IRepository<Category> _repository;

    public GetAllCategoriesQueryHandler(IRepository<Category> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Category>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}