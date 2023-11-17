using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries;

public abstract record BaseGetByExternalIdQuery<TEntity, TDto>
(
    int ExternalId,
    Expression<Func<TEntity, object?>>[] RelationsToInclude
) : IRequest<QueryReferenceResponse<TDto>>
    where TDto : class;