using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;

namespace Application.Common.Handlers;

public class BaseGetByExternalIdHandler<TEntity, TDto>
    : IRequestHandler<BaseGetByExternalIdQuery<TEntity, TDto>, TDto?>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;

    public BaseGetByExternalIdHandler(IRepository<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<TDto?> Handle(BaseGetByExternalIdQuery<TEntity, TDto> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.Id, cancellationToken);

        return entity is null ? null : _mappingService.Map<TEntity, TDto>(entity);
    }
}