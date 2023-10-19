using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Products.Dtos;
using Application.EntityManagement.Products.Queries;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Application.EntityManagement.Products.Handlers;

public class GetAllProductsByCategoryExternalIdQueryHandler
    : IRequestHandler<GetAllProductsByCategoryExternalIdQuery, QueryResponse>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public GetAllProductsByCategoryExternalIdQueryHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<QueryResponse> Handle(GetAllProductsByCategoryExternalIdQuery request, CancellationToken cancellationToken)
    {
        var category = await _unitOfWork.CategoryRepository.GetByExternalIdAsync(request.CategoryExternalId,
            new Func<Category, object?>[]
            {
                entity => entity.Products
            },
            cancellationToken);

        if (category is null)
        {
            return new QueryResponse
                (
                null,
                false,
                Messages.NotFound,
                HttpStatusCode.NotFound
                );
        }

        if (category.Products is null)
        {
            _logger.LogError(Messages.EntityRelationshipsRetrievalFailed, DateTime.UtcNow, typeof(Category), typeof(GetAllProductsByCategoryExternalIdQueryHandler));

            return new QueryResponse
                (
                null,
                false,
                Messages.InternalServerError,
                HttpStatusCode.InternalServerError
                );
        }

        var productDtos = _mappingService.Map<ICollection<Product>, ICollection<ProductDto>>(category.Products);

        if (productDtos is not null)
        {
            return new QueryResponse
                (
                productDtos.Paginate(request.Pagination),
                true,
                Messages.SuccessfullyRetrieved,
                HttpStatusCode.OK
                );
        }

        _logger.LogError(Messages.MappingFailed, DateTime.UtcNow, typeof(ICollection<Product>), typeof(GetAllProductsByCategoryExternalIdQueryHandler));

        return new QueryResponse
            (
            null,
            false,
            Messages.InternalServerError,
            HttpStatusCode.InternalServerError
            );
    }
}