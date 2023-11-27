using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.OrderItems.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.OrderItems.Handlers;

public class DeleteOrderItemByExternalIdCommandHandler : IRequestHandler<DeleteOrderItemByExternalIdCommand, CommandResult>
{
    private readonly IRepository<OrderItem> _repository;
    private readonly ILogger _logger;

    public DeleteOrderItemByExternalIdCommandHandler(IRepository<OrderItem> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteOrderItemByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await _repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(OrderItem), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}