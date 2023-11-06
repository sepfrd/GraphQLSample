using Application.Common;
using Application.EntityManagement.Users.Dtos;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUserDtosQuery
(
    Pagination Pagination,
    Expression<Func<User, object?>>[] RelationsToInclude
) : IRequest<QueryReferenceResponse<IEnumerable<UserDto>>>;