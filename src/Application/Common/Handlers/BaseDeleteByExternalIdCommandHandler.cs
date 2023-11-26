using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseDeleteByExternalIdCommandHandler<TEntity> : IRequestHandler<BaseDeleteByExternalIdCommand, CommandResult>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly ILogger _logger;

    protected BaseDeleteByExternalIdCommandHandler(IRepository<TEntity> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(BaseDeleteByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await _repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseDeleteByExternalIdCommandHandler<TEntity>));

        return CommandResult.Failure(Messages.InternalServerError);
    }

    public abstract Task<bool> DeleteRelationsAsync(TEntity entity, CancellationToken cancellationToken = default);
}