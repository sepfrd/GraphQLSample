using System.Linq.Expressions;
using Application.Common;
using Domain.Entities;
using MediatR;

namespace Application.EntityManagement.Roles.Queries;

public record GetAllRolesQuery(Expression<Func<Role, bool>>? Filter = null)
    : IRequest<QueryResponse<IEnumerable<Role>>>;