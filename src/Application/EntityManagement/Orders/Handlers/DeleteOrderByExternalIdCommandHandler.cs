using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Orders.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class DeleteOrderByExternalIdCommandHandler : IRequestHandler<DeleteOrderByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Order> _repository;
    private readonly ILogger _logger;

    public DeleteOrderByExternalIdCommandHandler(IRepository<Order> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteOrderByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await _repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Order), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}