using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Events;
using Application.EntityManagement.Comments.Queries;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Comments;

public class CommentService
{
    private readonly IMediator _mediator;

    public CommentService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination();

        var commentsQuery = new GetAllCommentsQuery(pagination, comment => comment.ExternalId == externalId);

        var commentResult = await _mediator.Send(commentsQuery, cancellationToken);

        if (!commentResult.IsSuccessful ||
            commentResult.Data is null ||
            !commentResult.Data.Any())
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deleteCommentCommand = new DeleteCommentByExternalIdCommand(externalId);

        await _mediator.Send(deleteCommentCommand, cancellationToken);

        var commentDeletedEvent = new CommentDeletedEvent(commentResult.Data.First());

        await _mediator.Publish(commentDeletedEvent, cancellationToken);

        return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
    }
}