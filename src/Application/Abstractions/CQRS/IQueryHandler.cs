using NoteKeeper.Domain.Common;

namespace Application.Abstractions.CQRS;

public interface IQueryHandler<in TQuery, TResult>
    where TQuery : IQuery
    where TResult : DomainResult
{
    Task<TResult> HandleAsync(TQuery query, CancellationToken cancellationToken);
}