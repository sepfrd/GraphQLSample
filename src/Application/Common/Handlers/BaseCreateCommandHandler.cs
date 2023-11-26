using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Common.Handlers;

public abstract class BaseCreateCommandHandler<TEntity, TDto> : IRequestHandler<BaseCreateCommand<TDto>, CommandResult>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    protected BaseCreateCommandHandler(IRepository<TEntity> repository, IMappingService mappingService, ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(BaseCreateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var entity = _mappingService.Map<TDto, TEntity>(request.Dto);

        if (entity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseCreateCommandHandler<TEntity, TDto>));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseCreateCommandHandler<TEntity, TDto>));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}