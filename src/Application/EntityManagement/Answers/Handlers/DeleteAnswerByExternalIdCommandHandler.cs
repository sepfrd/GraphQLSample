using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByExternalIdCommandHandler(IRepository<Answer> repository, ILogger logger)
    : IRequestHandler<DeleteAnswerByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteAnswerByExternalIdCommand request, CancellationToken cancellationToken)
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

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Answer), typeof(DeleteAnswerByExternalIdCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}