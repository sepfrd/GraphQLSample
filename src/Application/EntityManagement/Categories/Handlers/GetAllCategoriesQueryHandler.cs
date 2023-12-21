using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Categories.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Categories.Handlers;

public sealed class GetAllCategoriesQueryHandler(IRepository<Category> repository)
    : IRequestHandler<GetAllCategoriesQuery, QueryResponse<IEnumerable<Category>>>
{
    public async Task<QueryResponse<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Category>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}