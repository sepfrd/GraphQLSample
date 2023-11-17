using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseDeleteByInternalIdCommandHandler<TEntity>(IRepository<TEntity> repository, ILogger logger)
    : IRequestHandler<BaseDeleteByInternalIdCommand, CommandResult>
    where TEntity : BaseEntity
{
    public virtual async Task<CommandResult> Handle(BaseDeleteByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseDeleteByInternalIdCommandHandler<TEntity>));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}