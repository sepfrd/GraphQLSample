using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllQueryHandler<TEntity>
    : IRequestHandler<BaseGetAllQuery<TEntity>, QueryResponse>
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
    
    public virtual async Task<QueryResponse> Handle(BaseGetAllQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(null, request.RelationsToInclude, cancellationToken);

        var paginatedEntities = entities.Paginate(request.Pagination);
        
        return new QueryResponse
            (
            paginatedEntities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}