using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Categories.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Categories.Handlers;

public class GetAllCategoryDtosQueryHandler(
        IRepository<Category> categoryRepository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<GetAllCategoryDtosQuery, QueryReferenceResponse<IEnumerable<CategoryDto>>>
{
    public async Task<QueryReferenceResponse<IEnumerable<CategoryDto>>> Handle(GetAllCategoryDtosQuery request, CancellationToken cancellationToken)
    {
        var categories = await categoryRepository.GetAllAsync(null, cancellationToken);

        var categoryDtos = mappingService.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

        if (categoryDtos is not null)
        {
            return new QueryReferenceResponse<IEnumerable<CategoryDto>>(
                categoryDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(IEnumerable<Category>), typeof(GetAllCategoryDtosQueryHandler));

        return new QueryReferenceResponse<IEnumerable<CategoryDto>>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}