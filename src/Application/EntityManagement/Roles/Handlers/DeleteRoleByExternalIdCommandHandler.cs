using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class DeleteRoleByExternalIdCommandHandler(
        IRepository<Role> repository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Role>(repository, logger);