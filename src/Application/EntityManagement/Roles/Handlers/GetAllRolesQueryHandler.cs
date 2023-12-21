using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Roles.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Roles.Handlers;

public sealed class GetAllRolesQueryHandler(IRepository<Role> repository)
    : IRequestHandler<GetAllRolesQuery, QueryResponse<IEnumerable<Role>>>
{
    public async Task<QueryResponse<IEnumerable<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Role>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}