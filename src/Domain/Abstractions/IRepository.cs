using System.Linq.Expressions;
using Domain.Common;

namespace Domain.Abstractions;

public interface
    IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByInternalIdAsync(Guid internalId, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}