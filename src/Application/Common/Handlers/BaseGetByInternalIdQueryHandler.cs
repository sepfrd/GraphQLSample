using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByInternalIdQueryHandler<TEntity>(IRepository<TEntity> repository)
    : IRequestHandler<BaseGetByInternalIdQuery<TEntity>, QueryReferenceResponse<TEntity>>
    where TEntity : BaseEntity
{
    public virtual async Task<QueryReferenceResponse<TEntity>> Handle(BaseGetByInternalIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByInternalIdAsync(request.InternalId, cancellationToken, request.RelationsToInclude);

        if (entity is null)
        {
            return new QueryReferenceResponse<TEntity>
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        return new QueryReferenceResponse<TEntity>
            (
            entity,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}