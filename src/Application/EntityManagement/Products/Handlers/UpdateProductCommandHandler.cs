using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Products.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, CommandResult>
{
    private readonly IRepository<Product> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateProductCommandHandler(
        IRepository<Product> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = _mappingService.Map(request.ProductDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Product),
                typeof(UpdateProductCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Product),
            typeof(UpdateProductCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}