using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries;

public abstract record BaseGetAllQuery<TEntity>
(
    Pagination Pagination,
    Expression<Func<TEntity, object?>>[] RelationsToInclude
) : IRequest<QueryReferenceResponse<IEnumerable<TEntity>>>;