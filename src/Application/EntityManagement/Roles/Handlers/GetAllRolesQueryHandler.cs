using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Roles.Handlers;

public class GetAllRolesQueryHandler(IRepository<Role> repository) : BaseGetAllQueryHandler<Role>(repository);