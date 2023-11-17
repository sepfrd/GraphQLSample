using Application.Abstractions;
using Application.Common.Queries;
using Domain.Abstractions;
using Domain.Common;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.Common.Handlers;

public abstract class BaseGetAllDtosQueryHandler<TEntity, TDto>(
        IRepository<TEntity> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<BaseGetAllDtosQuery<TEntity, TDto>, QueryReferenceResponse<IEnumerable<TDto>>>
    where TEntity : BaseEntity
{

    public virtual async Task<QueryReferenceResponse<IEnumerable<TDto>>> Handle(BaseGetAllDtosQuery<TEntity, TDto> request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllAsync(null, cancellationToken, request.RelationsToInclude);

        var paginatedEntities = entities.Paginate(request.Pagination);

        if (paginatedEntities.Count == 0)
        {
            return new QueryReferenceResponse<IEnumerable<TDto>>
                (
                Array.Empty<TDto>(),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        var dtos = mappingService.Map<List<TEntity>, List<TDto>>(paginatedEntities);

        if (dtos is not null && dtos.Count != 0)
        {
            return new QueryReferenceResponse<IEnumerable<TDto>>
                (
                dtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(TEntity), typeof(BaseGetAllDtosQueryHandler<TEntity, TDto>));

        return new QueryReferenceResponse<IEnumerable<TDto>>
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}