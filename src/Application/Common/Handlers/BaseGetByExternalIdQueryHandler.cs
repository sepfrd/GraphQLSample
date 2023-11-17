using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetByExternalIdQueryHandler<TEntity, TDto>(
        IRepository<TEntity> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<BaseGetByExternalIdQuery<TEntity, TDto>, QueryReferenceResponse<TDto>>
    where TEntity : BaseEntity
    where TDto : class
{
    public virtual async Task<QueryReferenceResponse<TDto>> Handle(BaseGetByExternalIdQuery<TEntity, TDto> request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken, request.RelationsToInclude);

        if (entity is null)
        {
            return new QueryReferenceResponse<TDto>
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        var dto = mappingService.Map<TEntity, TDto>(entity);

        if (dto is not null)
        {
            return new QueryReferenceResponse<TDto>
                (
                dto,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseGetByExternalIdQueryHandler<TEntity, TDto>));

        return new QueryReferenceResponse<TDto>(Message: Messages.InternalServerError, HttpStatusCode: HttpStatusCode.InternalServerError);
    }
}