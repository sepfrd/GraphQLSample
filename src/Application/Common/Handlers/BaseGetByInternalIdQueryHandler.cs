using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByInternalIdQueryHandler<TEntity>
    : IRequestHandler<BaseGetByInternalIdQuery<TEntity>, QueryResponse>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;

    protected BaseGetByInternalIdQueryHandler(IUnitOfWork unitOfWork)
    {
        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TEntity>);

        _repository = (IRepository<TEntity>)repositoryInterface;
    }

    public virtual async Task<QueryResponse> Handle(BaseGetByInternalIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByInternalIdAsync(request.InternalId, request.RelationsToInclude, cancellationToken);

        if (entity is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        return new QueryResponse
            (
            entity,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}