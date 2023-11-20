using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByInternalIdQuery
(
    Guid InternalId,
    Expression<Func<User, object?>>[]? RelationsToInclude
) : IRequest<QueryReferenceResponse<User>>;