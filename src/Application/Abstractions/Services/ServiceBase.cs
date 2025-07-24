using System.Linq.Expressions;
using Application.Abstractions.Repositories;
using Application.Common;
using Domain.Abstractions;
using Microsoft.AspNetCore.Http;

namespace Application.Abstractions.Services;

public abstract class ServiceBase<TEntity, TDto> : IServiceBase<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    private readonly IRepositoryBase<TEntity> _repository;
    private readonly IMappingService _mappingService;

    protected ServiceBase(IRepositoryBase<TEntity> repository, IMappingService mappingService)
    {
        _repository = repository;
        _mappingService = mappingService;
    }

    public async Task<IEnumerable<TDto>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filterExpression = null,
        CancellationToken cancellationToken = default)
    {
        var entities = await _repository.GetAllAsync(filterExpression, cancellationToken);

        var dtos = _mappingService.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entities);

        return dtos;
    }

    public async Task<DomainResult<TDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return DomainResult<TDto>.Failure(Errors.NotFound, StatusCodes.Status404NotFound);
        }

        var dto = _mappingService.Map<TEntity, TDto>(entity);

        return DomainResult<TDto>.Success(dto);
    }

    public async Task<DomainResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default)
    {
        var entity = await _repository.GetByIdAsync(id, cancellationToken);

        if (entity is null)
        {
            return DomainResult.Failure(Errors.NotFound, StatusCodes.Status404NotFound);
        }

        var deletedEntity = await _repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is null)
        {
            return DomainResult.Failure(Errors.InternalServerError, StatusCodes.Status500InternalServerError);
        }

        return DomainResult.Success();
    }
}