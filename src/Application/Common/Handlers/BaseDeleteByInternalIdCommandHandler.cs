using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseDeleteByInternalIdCommandHandler<TEntity, TDto>
    : IRequestHandler<BaseDeleteByInternalIdCommand, CommandResult>
    where TEntity : BaseEntity
    where TDto : class
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

        if (deletedEntity is null)
        {
            _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(TEntity));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}