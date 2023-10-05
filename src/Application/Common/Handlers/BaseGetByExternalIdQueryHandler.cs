using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.Common.Handlers;

public abstract class BaseGetByExternalIdQueryHandler<TEntity, TDto>
    : IRequestHandler<BaseGetByExternalIdQuery<TDto>, TDto?>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;

    protected BaseGetByExternalIdQueryHandler(IRepository<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<TDto?> Handle(BaseGetByExternalIdQuery<TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.Id, cancellationToken);

        return entity is null ? null : _mappingService.Map<TEntity, TDto>(entity);
    }
}