using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Products.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Products.Handlers;

public class UpdateProductCommandHandler(
        IRepository<Product> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdateProductCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = mappingService.Map(request.ProductDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Product), typeof(UpdateProductCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Product), typeof(UpdateProductCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}