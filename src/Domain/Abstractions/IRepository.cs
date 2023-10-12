using Domain.Common;

namespace Domain.Abstractions;

public interface 
    IRepository<TEntity> where TEntity : BaseEntity
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<int> GenerateUniqueExternalIdAsync(CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(IEnumerable<Func<TEntity, object?>>? relationsToInclude = null, 
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByInternalIdAsync(Guid internalId,
        IEnumerable<Func<TEntity, object?>>? relationsToInclude = null, 
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByExternalIdAsync(int externalId,
        IEnumerable<Func<TEntity, object?>>? relationsToInclude = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteAsync(TEntity entity, CancellationToken cancellationToken = default);
}