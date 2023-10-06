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
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    protected BaseUpdateCommandHandler(IRepository<TEntity> repository, IMappingService mappingService, ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(BaseUpdateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var newEntity = _mappingService.Map(request.Dto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, GetType());

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is null)
        {
            _logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, GetType());

            return CommandResult.Failure(Messages.InternalServerError);
        }

        return CommandResult.Success(Messages.SuccessfullyUpdated);
    }
}