using System.Linq.Expressions;
using Domain.Common;

namespace Domain.Abstractions;

public interface
    IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> GenerateUniqueExternalIdAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>>? filter = null, Pagination? pagination = null, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByInternalIdAsync(Guid internalId, CancellationToken cancellationToken = default);

    Task<TEntity?> GetByExternalIdAsync(int externalId, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}