using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.UserRoles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class UpdateUserRoleCommandHandler(
        IRepository<UserRole> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdateUserRoleCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = mappingService.Map(request.UserRoleDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(UserRole), typeof(UpdateUserRoleCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(UserRole), typeof(UpdateUserRoleCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}