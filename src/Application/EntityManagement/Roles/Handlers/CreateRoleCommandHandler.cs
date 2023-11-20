using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Roles.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class CreateRoleCommandHandler(
        IRepository<Role> repository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Role, RoleDto>(repository, mappingService, logger);