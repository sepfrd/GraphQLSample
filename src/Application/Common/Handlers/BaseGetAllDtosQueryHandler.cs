using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllDtosQueryHandler<TEntity, TDto>
    : IRequestHandler<BaseGetAllDtosQuery, QueryResponse>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;

    protected BaseGetAllDtosQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService)
    {
        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TEntity>);
        
        _repository = (IRepository<TEntity>)repositoryInterface;
        _mappingService = mappingService;
    }

    public virtual async Task<QueryResponse> Handle(BaseGetAllDtosQuery request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(cancellationToken);

        var dtos = _mappingService.Map<IEnumerable<TEntity>, IEnumerable<TDto>>(entities);

        return new QueryResponse
            (
            dtos,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}