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
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = mappingService.Map(request.ProductDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Product), typeof(UpdateProductCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(Product), typeof(UpdateProductCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}