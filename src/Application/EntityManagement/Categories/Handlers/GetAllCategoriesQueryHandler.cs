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

public class GetAllCategoriesQueryHandler : IRequestHandler<GetAllCategoriesQuery, QueryReferenceResponse<IEnumerable<CategoryDto>>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllCategoriesQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryReferenceResponse<IEnumerable<CategoryDto>>> Handle(GetAllCategoriesQuery request, CancellationToken cancellationToken)
    {
        var categories = await _unitOfWork.CategoryRepository.GetAllAsync(null, cancellationToken);

        var categoryDtos = _mappingService.Map<IEnumerable<Category>, IEnumerable<CategoryDto>>(categories);

        if (categoryDtos is not null)
        {
            return new QueryReferenceResponse<IEnumerable<CategoryDto>>(
                categoryDtos,
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(IEnumerable<Category>), typeof(GetAllCategoriesQueryHandler));

        return new QueryReferenceResponse<IEnumerable<CategoryDto>>(
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}