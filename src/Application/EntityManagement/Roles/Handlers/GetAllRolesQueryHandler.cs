using Application.Common;
using Application.EntityManagement.Roles.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Roles.Handlers;

public class GetAllRolesQueryHandler(IRepository<Role> repository)
    : IRequestHandler<GetAllRolesQuery, QueryReferenceResponse<IEnumerable<Role>>>
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<Role>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}