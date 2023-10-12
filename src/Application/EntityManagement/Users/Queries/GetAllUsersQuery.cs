using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUsersQuery
(
    Pagination Pagination,
    IEnumerable<Func<User, object?>>? RelationsToInclude = null
) : IRequest<QueryResponse>;