#region

using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Questions.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Questions.Handlers;

public class DeleteQuestionByExternalIdCommandHandler(IRepository<Question> repository, ILogger logger)
    : IRequestHandler<DeleteQuestionByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteQuestionByExternalIdCommand request, CancellationToken cancellationToken)
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

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Question), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}