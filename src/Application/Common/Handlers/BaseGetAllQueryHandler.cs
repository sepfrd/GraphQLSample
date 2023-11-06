using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllQueryHandler<TEntity>
    : IRequestHandler<BaseGetAllQuery<TEntity>, QueryReferenceResponse<IEnumerable<TEntity>>>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;

    protected BaseGetAllQueryHandler(IUnitOfWork unitOfWork)
    {
        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TEntity>);

        _repository = (IRepository<TEntity>)repositoryInterface;
    }

    public virtual async Task<QueryReferenceResponse<IEnumerable<TEntity>>> Handle(BaseGetAllQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        return new QueryReferenceResponse<IEnumerable<TEntity>>
            (
            entities.Paginate(request.Pagination),
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}