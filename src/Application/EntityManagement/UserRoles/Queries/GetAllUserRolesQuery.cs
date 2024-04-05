using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.UserRoles.Queries;

public record GetAllUserRolesQuery(Expression<Func<UserRole, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<UserRole>>>;