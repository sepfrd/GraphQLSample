using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByInternalIdQueryHandler<TEntity> : IRequestHandler<BaseGetByInternalIdQuery<TEntity>, QueryReferenceResponse<TEntity>>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;

    protected BaseGetByInternalIdQueryHandler(IRepository<TEntity> repository)
    {
        _repository = repository;
    }

    public virtual async Task<QueryReferenceResponse<TEntity>> Handle(BaseGetByInternalIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (entity is null)
        {
            return new QueryReferenceResponse<TEntity>(
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound);
        }

        return new QueryReferenceResponse<TEntity>(
            entity,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK);
    }
}