#region

using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Comments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Comments.Handlers;

public class DeleteCommentByExternalIdCommandHandler : IRequestHandler<DeleteCommentByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Comment> _repository;
    private readonly ILogger _logger;

    public DeleteCommentByExternalIdCommandHandler(IRepository<Comment> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteCommentByExternalIdCommand request, CancellationToken cancellationToken)
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

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Comment), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}