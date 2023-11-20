using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Comments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class DeleteCommentByExternalIdCommandHandler(IRepository<Comment> repository, ILogger logger)
    : IRequestHandler<DeleteCommentByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeleteCommentByExternalIdCommand request, CancellationToken cancellationToken)
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

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Comment), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}