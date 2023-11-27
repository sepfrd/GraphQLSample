using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Events;
using Application.EntityManagement.Answers.Queries;
using Domain.Common;
using MediatR;

namespace Application.EntityManagement.Answers;

public class AnswerService
{
    private readonly IMediator _mediator;

    public AnswerService(IMediator mediator)
    {
        _mediator = mediator;
    }

    public async Task<CommandResult> DeleteByExternalIdAsync(int externalId, CancellationToken cancellationToken = default)
    {
        var pagination = new Pagination();

        var answersQuery = new GetAllAnswersQuery(pagination, answer => answer.ExternalId == externalId);

        var answerResult = await _mediator.Send(answersQuery, cancellationToken);

        if (!answerResult.IsSuccessful ||
            answerResult.Data is null ||
            !answerResult.Data.Any())
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deleteAnswerCommand = new DeleteAnswerByExternalIdCommand(externalId);

        await _mediator.Send(deleteAnswerCommand, cancellationToken);

        var answerDeletedEvent = new AnswerDeletedEvent(answerResult.Data.First());

        await _mediator.Publish(answerDeletedEvent, cancellationToken);
        
        return CommandResult.Success(Messages.SuccessfullyDeleted);
    }
}