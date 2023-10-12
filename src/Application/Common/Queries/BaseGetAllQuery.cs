using MediatR;

namespace Application.Common.Queries;

public abstract record BaseGetAllQuery<TEntity>
(
    Pagination Pagination,
    IEnumerable<Func<TEntity, object?>>? RelationsToInclude = null
) : IRequest<QueryResponse>;