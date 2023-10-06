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

    protected BaseDeleteByInternalIdCommandHandler(IRepository<TEntity> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(BaseDeleteByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByInternalIdAsync(request.Id, cancellationToken);

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