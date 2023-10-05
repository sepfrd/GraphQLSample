using Application.Abstractions;
using Application.Common.Commands;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.Common.Handlers;

public abstract class BaseCreateCommandHandler<TEntity, TDto>
    : IRequestHandler<BaseCreateCommand<TDto>, TDto?>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;

    protected BaseCreateCommandHandler(IRepository<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<TDto?> Handle(BaseCreateCommand<TDto> request, CancellationToken cancellationToken)
    {
        var entity = _mappingService.Map<TDto, TEntity>(request.Dto);

        if (entity is null)
        {
            return null;
        }

        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is null)
        {
            return null;
        }

        var dto = _mappingService.Map<TEntity, TDto>(createdEntity);

        return dto;
    }
}