using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Roles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class DeleteRoleByExternalIdCommandHandler(IRepository<Role> repository, ILogger logger)
    : IRequestHandler<DeleteRoleByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteRoleByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Role), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}