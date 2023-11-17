using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllQueryHandler<TEntity>(IRepository<TEntity> repository)
    : IRequestHandler<BaseGetAllQuery<TEntity>, QueryReferenceResponse<IEnumerable<TEntity>>>
    where TEntity : BaseEntity
{
    public virtual async Task<QueryReferenceResponse<IEnumerable<TEntity>>> Handle(BaseGetAllQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<TEntity>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}