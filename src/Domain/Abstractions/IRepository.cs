using Domain.Common;
using System.Linq.Expressions;

namespace Domain.Abstractions;

public interface
    IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> GenerateUniqueExternalIdAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, CancellationToken cancellationToken = default,
        params Expression<Func<TEntity, object?>>[]? includes);

    Task<TEntity?> GetByInternalIdAsync(Guid internalId, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[]? includes);

    Task<TEntity?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken = default, params Expression<Func<TEntity, object?>>[]? includes);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}