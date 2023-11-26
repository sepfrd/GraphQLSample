#region

using Application.Common;
using Domain.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

#endregion

namespace Application.EntityManagement.UserRoles.Queries;

public record GetAllUserRolesQuery(
        Pagination Pagination,
        Expression<Func<UserRole, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<UserRole>>>;