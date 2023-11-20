using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.UserRoles.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class CreateUserRoleCommandHandler(
        IRepository<UserRole> repository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<UserRole, UserRoleDto>(repository, mappingService, logger);