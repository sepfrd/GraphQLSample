using Domain.Common;

namespace Domain.Abstractions;

public interface IDbContext<TEntity> where TEntity : BaseEntity
{
    Task<TEntity> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}