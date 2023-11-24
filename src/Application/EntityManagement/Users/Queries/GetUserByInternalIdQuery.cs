using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByInternalIdQuery
(
    Guid InternalId,
    Expression<Func<User, object?>>[]? RelationsToInclude = null
) : IRequest<QueryReferenceResponse<User>>;