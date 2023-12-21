using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Commands;
using Application.EntityManagement.Products.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, CommandResult>
{
    private readonly IRepository<Product> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateProductCommandHandler(IRepository<Product> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = _mappingService.Map<ProductDto, Product>(request.ProductDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Product), typeof(CreateProductCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Product), typeof(CreateProductCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}