using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByExternalIdQuery
(
    int ExternalId,
    IEnumerable<Func<User, object?>>? RelationsToInclude = null
) : IRequest<QueryResponse>;