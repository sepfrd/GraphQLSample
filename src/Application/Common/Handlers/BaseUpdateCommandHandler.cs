using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseUpdateCommandHandler<TEntity, TDto>(
        IRepository<TEntity> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<BaseUpdateCommand<TDto>, CommandResult>
    where TEntity : BaseEntity
    where TDto : class
{
    public virtual async Task<CommandResult> Handle(BaseUpdateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = mappingService.Map(request.Dto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseUpdateCommandHandler<TEntity, TDto>));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseUpdateCommandHandler<TEntity, TDto>));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}