using Application.Common.Abstractions;
using Application.Common.Abstractions.Services;
using Domain.Abstractions;

namespace Application.Services;

public class ServiceBase<TEntity, TDto> : IServiceBase<TDto>
    where TEntity : DomainEntity
    where TDto : class
{
    private readonly IRepositoryBase<TEntity> _repository;
    private readonly IMappingService _mappingService;

    public ServiceBase(IRepositoryBase<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<DomainResult<IEnumerable<TDto>>> GetAllAsync(CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(cancellationToken: cancellationToken);

        var dtos = _mappingService.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entities);

        return DomainResult<IEnumerable<TDto>>.Success(dtos);
    }

    public async Task<DomainResult<TDto>> CreateOneAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mappingService.Map<TDto, TEntity>(dto);

        var createdEntity = await _repository.CreateAsync(entity, cancellationToken: cancellationToken);

        if (createdEntity is null)
        {
            return DomainResult<TDto>.Failure(new Error("500", "InternalServerError"));
        }

        var responseDto = _mappingService.Map<TEntity, TDto>(createdEntity);

        return DomainResult<TDto>.Success(responseDto);
    }

    public async Task<DomainResult<TDto>> UpdateAsync(TDto dto, CancellationToken cancellationToken = default)
    {
        var entity = _mappingService.Map<TDto, TEntity>(dto);

        entity.MarkAsUpdated();

        var updatedEntity = await _repository.UpdateAsync(entity, cancellationToken: cancellationToken);

        if (updatedEntity is null)
        {
            return DomainResult<TDto>.Failure(new Error("500", "InternalServerError"));
        }

        var responseDto = _mappingService.Map<TEntity, TDto>(updatedEntity);

        return DomainResult<TDto>.Success(responseDto);
    }

    public async Task<DomainResult> DeleteAsync(Guid uuid, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByUuidAsync(uuid, cancellationToken);

        if (entity is null)
        {
            return DomainResult.Failure(new Error("404", "NotFound"));
        }

        var deletedEntity = await _repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is null)
        {
            return DomainResult.Failure(new Error("500", "InternalServerError"));
        }

        return DomainResult.Success();
    }
}