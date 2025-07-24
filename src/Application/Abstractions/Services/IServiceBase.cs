using System.Linq.Expressions;
using Domain.Abstractions;

namespace Application.Abstractions.Services;

public interface IServiceBase<TEntity, TDto>
    where TEntity : class
    where TDto : class
{
    Task<IEnumerable<TDto>> GetAllAsync(Expression<Func<TEntity, bool>>? filterExpression = null, CancellationToken cancellationToken = default);

    Task<DomainResult<TDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<DomainResult> DeleteAsync(Guid id, CancellationToken cancellationToken = default);
}