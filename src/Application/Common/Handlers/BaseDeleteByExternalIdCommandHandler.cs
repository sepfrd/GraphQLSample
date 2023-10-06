using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseDeleteByExternalIdCommandHandler<TEntity>
    : IRequestHandler<BaseDeleteByExternalIdCommand, CommandResult>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly ILogger _logger;

    protected BaseDeleteByExternalIdCommandHandler(IRepository<TEntity> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(BaseDeleteByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await _repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is null)
        {
            _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, GetType());
            
            return CommandResult.Failure(Messages.InternalServerError);
        }

        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}