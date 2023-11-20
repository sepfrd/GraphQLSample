using Application.Common;
using Domain.Entities;
using MediatR;
using System.Linq.Expressions;

namespace Application.EntityManagement.Roles.Queries;

public record GetAllRolesQuery(
        Pagination Pagination,
        Expression<Func<Role, object?>>[]? RelationsToInclude = null,
        Expression<Func<Role, bool>>? Filter = null)
    : IRequest<QueryReferenceResponse<IEnumerable<Role>>>;