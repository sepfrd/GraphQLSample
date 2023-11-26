using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByExternalIdQueryHandler<TEntity>
    : IRequestHandler<BaseGetByExternalIdQuery<TEntity>, QueryReferenceResponse<TEntity>>
    where TEntity : BaseEntity
{
    private readonly IRepository<TEntity> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    protected BaseGetByExternalIdQueryHandler(IRepository<TEntity> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<QueryReferenceResponse<TEntity>> Handle(BaseGetByExternalIdQuery<TEntity> request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return new QueryReferenceResponse<TEntity>(
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound);
        }

        var dto = _mappingService.Map<TEntity, TEntity>(entity);

        if (dto is not null)
        {
            return new QueryReferenceResponse<TEntity>(
                dto,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK);
        }

        _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseGetByExternalIdQueryHandler<TEntity>));

        return new QueryReferenceResponse<TEntity>(Message: Messages.InternalServerError, HttpStatusCode: HttpStatusCode.InternalServerError);
    }
}