using Application.Common.Abstractions;
using Application.Common.Abstractions.Services;
using Domain.Abstractions;

namespace Application.Services;

public class ServiceBase<TEntity, TDto> : IServiceBase<TEntity, TDto>
    where TEntity : DomainEntity
    where TDto : IHasUuid
{
    private readonly IRepositoryBase<TEntity> _repository;
    private readonly IMappingService _mappingService;

    public ServiceBase(IRepositoryBase<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default) =>
        await _repository.GetAllAsync(cancellationToken: cancellationToken);

    public async Task<TEntity?> CreateOneAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mappingService.Map<TDto, TEntity>(dto);

        return await _repository.CreateAsync(entity, cancellationToken: cancellationToken);
    }

    public async Task<TEntity?> UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByUuidAsync(dto.Uuid, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        var updatedEntity = _mappingService.Map(dto, entity);

        updatedEntity.MarkAsUpdated();

        return await _repository.UpdateAsync(updatedEntity, cancellationToken);
    }

    public async Task<TEntity?> DeleteAsync(Guid uuid, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByUuidAsync(uuid, cancellationToken);

        if (entity is null)
        {
            return null;
        }

        return await _repository.DeleteOneAsync(entity, cancellationToken);
    }
}