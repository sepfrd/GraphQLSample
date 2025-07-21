using System.Linq.Expressions;
using Domain.Abstractions;

namespace Application.Common.Abstractions;

public interface IRepositoryBase<TEntity> where TEntity : IHasUuid
{
    Task<TEntity?> CreateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<IEnumerable<TEntity>> GetAllAsync(
        Expression<Func<TEntity, bool>>? filter = null,
        CancellationToken cancellationToken = default);

    Task<TEntity?> GetByUuidAsync(Guid uuid, CancellationToken cancellationToken = default);

    Task<TEntity?> UpdateAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task<TEntity?> DeleteOneAsync(TEntity entity, CancellationToken cancellationToken = default);

    Task DeleteManyAsync(IEnumerable<TEntity> entities, CancellationToken cancellationToken = default);
}