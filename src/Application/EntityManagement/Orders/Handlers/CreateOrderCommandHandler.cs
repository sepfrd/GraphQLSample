using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Orders.Commands;
using Application.EntityManagement.Orders.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class CreateOrderCommandHandler(
        IRepository<Order> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateOrderCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<OrderDto, Order>(request.OrderDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Order), typeof(CreateOrderCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Order), typeof(CreateOrderCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}