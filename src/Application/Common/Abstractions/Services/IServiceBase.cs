namespace Application.Common.Abstractions.Services;

public interface IServiceBase<TEntity, in TDto>
{
    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> CreateOneAsync(TDto dto, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TDto dto, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteAsync(Guid uuid, CancellationToken cancellationToken = default);
}