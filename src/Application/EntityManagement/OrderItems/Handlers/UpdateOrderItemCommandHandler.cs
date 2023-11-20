using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.OrderItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class UpdateOrderItemCommandHandler(
        IRepository<OrderItem> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdateOrderItemCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = mappingService.Map(request.OrderItemDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(OrderItem), typeof(UpdateOrderItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(OrderItem), typeof(UpdateOrderItemCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}