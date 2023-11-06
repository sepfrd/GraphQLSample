using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries;

public abstract record BaseGetAllDtosQuery<TEntity, TDto>
(
    Pagination Pagination,
    Expression<Func<TEntity, object?>>[] RelationsToInclude
) : IRequest<QueryReferenceResponse<IEnumerable<TDto>>>;