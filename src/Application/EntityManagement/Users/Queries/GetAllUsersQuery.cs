using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Users.Queries;

public sealed record GetAllUsersQuery(Expression<Func<User, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<User>>>;