using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUserDtosQuery
(
    Pagination Pagination,
    Expression<Func<User, object?>>[] RelationsToInclude
) : IRequest<QueryResponse>;