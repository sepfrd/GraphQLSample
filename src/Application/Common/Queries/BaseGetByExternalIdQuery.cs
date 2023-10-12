using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery<TEntity>
(
    int ExternalId,
    IEnumerable<Func<TEntity, object?>> ? RelationsToInclude = null
) : IRequest<QueryResponse>;