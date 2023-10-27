using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery<TEntity>
(
    int ExternalId,
    Expression<Func<TEntity, object?>>[] RelationsToInclude
) : IRequest<QueryResponse>;