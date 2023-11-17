using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseCreateCommandHandler<TEntity, TDto>(IRepository<TEntity> repository, IMappingService mappingService, ILogger logger)
    : IRequestHandler<BaseCreateCommand<TDto>, CommandResult>
    where TEntity : BaseEntity
    where TDto : class
{
    public virtual async Task<CommandResult> Handle(BaseCreateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<TDto, TEntity>(request.Dto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseCreateCommandHandler<TEntity, TDto>));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseCreateCommandHandler<TEntity, TDto>));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}