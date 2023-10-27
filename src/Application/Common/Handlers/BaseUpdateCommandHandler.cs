using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseUpdateCommandHandler<TEntity, TDto>
    : IRequestHandler<BaseUpdateCommand<TDto>, CommandResult>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    protected BaseUpdateCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TEntity>);

        _repository = (IRepository<TEntity>)repositoryInterface;
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(BaseUpdateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = _mappingService.Map(request.Dto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseUpdateCommandHandler<TEntity, TDto>));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        _logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseUpdateCommandHandler<TEntity, TDto>));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}