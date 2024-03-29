using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.UserRoles.Queries;

public record GetAllUserRolesQuery(
        Pagination Pagination,
        Expression<Func<UserRole, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<UserRole>>>;