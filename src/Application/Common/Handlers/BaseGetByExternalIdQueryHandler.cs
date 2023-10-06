using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByExternalIdQueryHandler<TEntity, TDto>
    : IRequestHandler<BaseGetByExternalIdQuery, QueryResponse>
    where TEntity : BaseEntity
    where TDto : class
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    protected BaseGetByExternalIdQueryHandler(IRepository<TEntity> repository, IMappingService mappingService, ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(BaseGetByExternalIdQuery request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.Id, cancellationToken);

        if (entity is null)
        {
            return new QueryResponse(Message: Messages.NotFound, HttpStatusCode: HttpStatusCode.NotFound);
        }

        var dto = _mappingService.Map<TEntity, TDto>(entity);

        if (dto is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, GetType());

            return new QueryResponse(Message: Messages.InternalServerError, HttpStatusCode: HttpStatusCode.InternalServerError);
        }

        return new QueryResponse
            (
            dto,
            true,
            Messages.SuccessfullyRetrieved,
            HttpStatusCode.OK
            );
    }
}