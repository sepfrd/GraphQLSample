#region

using Application.Common;
using Application.EntityManagement.UserRoles.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

#endregion

namespace Application.EntityManagement.UserRoles.Handlers;

public sealed class GetAllUserRolesQueryHandler(IRepository<UserRole> repository)
    : IRequestHandler<GetAllUserRolesQuery, QueryReferenceResponse<IEnumerable<UserRole>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<UserRole>>> Handle(GetAllUserRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, request.Pagination, cancellationToken);

        return new QueryReferenceResponse<IEnumerable<UserRole>>(
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}