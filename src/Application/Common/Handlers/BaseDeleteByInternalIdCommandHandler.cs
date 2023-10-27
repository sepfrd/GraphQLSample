using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseDeleteByInternalIdCommandHandler<TEntity>
    : IRequestHandler<BaseDeleteByInternalIdCommand, CommandResult>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly ILogger _logger;

    protected BaseDeleteByInternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TEntity>);

        _repository = (IRepository<TEntity>)repositoryInterface;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(BaseDeleteByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await _repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseDeleteByInternalIdCommandHandler<TEntity>));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}