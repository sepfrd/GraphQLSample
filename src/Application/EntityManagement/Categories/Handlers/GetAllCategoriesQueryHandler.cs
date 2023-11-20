using Application.Common;
using Application.EntityManagement.Categories.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Categories.Handlers;

public class GetAllCategoriesQueryHandler(IRepository<Category> repository)
    : IRequestHandler<GetAllCategoriesQuery, QueryReferenceResponse<IEnumerable<Category>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Category>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<Category>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}