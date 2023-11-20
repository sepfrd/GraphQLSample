using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.UserRoles.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class UpdateUserRoleCommandHandler(
        IRepository<UserRole> repository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<UserRole, UserRoleDto>(repository, mappingService, logger);