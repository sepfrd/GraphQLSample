#region

using Application.Common;
using Application.EntityManagement.Categories.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.Categories.Handlers;

public sealed class GetAllCategoriesQueryHandler(IRepository<Category> repository)
    : IRequestHandler<GetAllCategoriesQuery, QueryReferenceResponse<IEnumerable<Category>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Category>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}