using System.Linq.Expressions;
using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUsersQuery(
        Pagination Pagination,
        Expression<Func<User, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<User>>>;