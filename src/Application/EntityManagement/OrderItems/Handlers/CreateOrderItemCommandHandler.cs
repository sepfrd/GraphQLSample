using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.OrderItems.Commands;
using Application.EntityManagement.OrderItems.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class CreateOrderItemCommandHandler(
        IRepository<OrderItem> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateOrderItemCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateOrderItemCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<OrderItemDto, OrderItem>(request.OrderItemDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(OrderItem), typeof(CreateOrderItemCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(OrderItem), typeof(CreateOrderItemCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}