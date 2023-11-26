#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Orders.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Orders.Handlers;

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand, CommandResult>
{
    private readonly IRepository<Order> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateOrderCommandHandler(IRepository<Order> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = _mappingService.Map(request.CreateOrderDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Order), typeof(UpdateOrderCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        _logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(Order), typeof(UpdateOrderCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}