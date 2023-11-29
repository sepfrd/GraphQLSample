using Application.Common;
using Application.EntityManagement.Roles.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Roles.Handlers;

public sealed class GetAllRolesQueryHandler(IRepository<Role> repository)
    : IRequestHandler<GetAllRolesQuery, QueryReferenceResponse<IEnumerable<Role>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<Role>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}