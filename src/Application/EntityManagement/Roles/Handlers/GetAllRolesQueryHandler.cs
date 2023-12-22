using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Roles.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using System.Net;

namespace Application.EntityManagement.Roles.Handlers;

public sealed class GetAllRolesQueryHandler : IRequestHandler<GetAllRolesQuery, QueryResponse<IEnumerable<Role>>>
{
    private readonly IRepository<Role> _repository;

    public GetAllRolesQueryHandler(IRepository<Role> repository)
    {
        _repository = repository;
    }

    public async Task<QueryResponse<IEnumerable<Role>>> Handle(GetAllRolesQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.Filter, request.Pagination, cancellationToken);

        return new QueryResponse<IEnumerable<Role>>(
            entities,
            true,
            MessageConstants.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}