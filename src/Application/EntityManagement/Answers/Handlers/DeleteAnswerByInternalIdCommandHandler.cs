using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByInternalIdCommandHandler : IRequestHandler<DeleteAnswerByInternalIdCommand, CommandResult>
{
    private readonly IRepository<Answer> _repository;
    private readonly ILogger _logger;

    public DeleteAnswerByInternalIdCommandHandler(IRepository<Answer> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteAnswerByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await _repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Answer), typeof(DeleteAnswerByInternalIdCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}