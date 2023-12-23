using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Dtos;
using Application.EntityManagement.Products.Dtos.ProductDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CommandResult>
{
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Category> _categoryRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateProductCommandHandler(
        IRepository<Product> productRepository,
        IRepository<Category> categoryRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _productRepository = productRepository;
        _categoryRepository = categoryRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetByExternalIdAsync(request.ProductDto.CategoryExternalId, cancellationToken);

        if (category is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var entity = _mappingService.Map<ProductDto, Product>(request.ProductDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Product), typeof(CreateProductCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.CategoryId = category.InternalId;

        var createdEntity = await _productRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Product), typeof(CreateProductCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}