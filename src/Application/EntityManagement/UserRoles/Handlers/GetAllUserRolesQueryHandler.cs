using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.UserRoles.Handlers;

public class GetAllUserRolesQueryHandler(IRepository<UserRole> repository)
    : BaseGetAllQueryHandler<UserRole>(repository);