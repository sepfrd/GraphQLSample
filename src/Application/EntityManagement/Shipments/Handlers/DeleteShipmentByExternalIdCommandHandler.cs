using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Shipments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Shipments.Handlers;

public class DeleteShipmentByExternalIdCommandHandler(IRepository<Shipment> repository, ILogger logger)
    : IRequestHandler<DeleteShipmentByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteShipmentByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Shipment), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}