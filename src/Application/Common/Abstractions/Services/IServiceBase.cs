using Domain.Abstractions;

namespace Application.Common.Abstractions.Services;

public interface IServiceBase<T> where T : class
{
    Task<DomainResult<IEnumerable<T>>> GetAllAsync(CancellationToken cancellationToken = default);

    Task<DomainResult<T>> CreateOneAsync(T entity, CancellationToken cancellationToken = default);

    Task<DomainResult<T>> UpdateAsync(T entity, CancellationToken cancellationToken = default);

    Task<DomainResult> DeleteAsync(Guid uuid, CancellationToken cancellationToken = default);
}