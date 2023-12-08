using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Payments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class DeletePaymentByExternalIdCommandHandler(IRepository<Payment> repository, ILogger logger)
    : IRequestHandler<DeletePaymentByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeletePaymentByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deletedEntity = await repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(Payment), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}