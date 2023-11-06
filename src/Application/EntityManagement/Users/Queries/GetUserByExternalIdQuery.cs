using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetUserByExternalIdQuery
(
    int ExternalId,
    Expression<Func<User, object?>>[] RelationsToInclude
) : IRequest<QueryReferenceResponse<UserDto>>;