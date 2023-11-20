using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Roles.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class UpdateRoleCommandHandler(
        IRepository<Role> repository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Role, RoleDto>(repository, mappingService, logger);