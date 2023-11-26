#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Roles.Commands;
using Application.EntityManagement.Roles.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Roles.Handlers;

public class CreateRoleCommandHandler(
        IRepository<Role> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateRoleCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<RoleDto, Role>(request.RoleDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Role), typeof(CreateRoleCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Role), typeof(CreateRoleCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}