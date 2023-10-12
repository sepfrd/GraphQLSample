using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByInternalIdQuery<TEntity>
(
    Guid InternalId,
    IEnumerable<Func<TEntity, object?>>? RelationsToInclude = null
) : IRequest<QueryResponse>;