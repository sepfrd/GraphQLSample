using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.Common.Handlers;

public abstract class BaseUpdateCommandHandler<TEntity, TDto>
    : IRequestHandler<BaseUpdateCommand<TDto>, TDto?>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;

    protected BaseUpdateCommandHandler(IRepository<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<TDto?> Handle(BaseUpdateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var newEntity = _mappingService.Map<TDto, TEntity>(request.Dto);

        if (newEntity is null)
        {
            return null;
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is null)
        {
            return null;
        }

        var updatedEntityDto = _mappingService.Map<TEntity, TDto>(updatedEntity);

        return updatedEntityDto;
    }
}