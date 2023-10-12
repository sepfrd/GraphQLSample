using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllDtosQueryHandler<TEntity, TDto>
    : IRequestHandler<BaseGetAllDtosQuery<TEntity>, QueryResponse>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    protected BaseGetAllDtosQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        var repositoryInterface = unitOfWork
            .Repositories
            .First(repository => repository is IRepository<TEntity>);

        _repository = (IRepository<TEntity>)repositoryInterface;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<QueryResponse> Handle(BaseGetAllDtosQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entities = await _repository.GetAllAsync(request.RelationsToInclude, cancellationToken);

        var paginatedEntities = entities.Paginate(request.Pagination);

        if (paginatedEntities.Count == 0)
        {
            return new QueryResponse
                (
                Array.Empty<TDto>(),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        var dtos = _mappingService.Map<List<TEntity>, List<TDto>>(paginatedEntities);

        if (dtos is not null && dtos.Any())
        {
            return new QueryResponse
                (
                dtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseGetAllDtosQueryHandler<TEntity, TDto>));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}