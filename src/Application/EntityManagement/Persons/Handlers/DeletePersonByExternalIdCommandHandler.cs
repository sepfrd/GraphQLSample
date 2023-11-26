#region

using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Persons.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Persons.Handlers;

public class DeletePersonByExternalIdCommandHandler(IRepository<Person> repository, ILogger logger)
    : IRequestHandler<DeletePersonByExternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeletePersonByExternalIdCommand request, CancellationToken cancellationToken)
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

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Person), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}