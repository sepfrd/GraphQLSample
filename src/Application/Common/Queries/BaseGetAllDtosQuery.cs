using MediatR;
using System.Linq.Expressions;

namespace Application.Common.Queries;

public abstract record BaseGetAllDtosQuery<TEntity>
(
    Pagination Pagination,
    Expression<Func<TEntity, object?>>[] RelationsToInclude
) : IRequest<QueryResponse>;