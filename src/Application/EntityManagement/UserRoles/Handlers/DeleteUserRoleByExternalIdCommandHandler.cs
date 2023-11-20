using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class DeleteUserRoleByExternalIdCommandHandler(
        IRepository<UserRole> repository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<UserRole>(repository, logger);