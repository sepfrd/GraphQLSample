using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllQueryHandler<TEntity>
    : IRequestHandler<BaseGetAllQuery, QueryResponse>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;

    protected BaseGetAllQueryHandler(IRepository<TEntity> repository) =>
        _repository = repository;
    
    public async Task<QueryResponse> Handle(BaseGetAllQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);
        
        return new QueryResponse
            (
            entities,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}