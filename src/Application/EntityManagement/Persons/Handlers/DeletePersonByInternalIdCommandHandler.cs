#region

using Application.Common;
using Application.EntityManagement.Persons.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Persons.Handlers;

public class DeletePersonByInternalIdCommandHandler(IRepository<Person> repository, ILogger logger)
    : IRequestHandler<DeletePersonByInternalIdCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(DeletePersonByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(Messages.NotFound);
        }

        var deletedEntity = await repository.DeleteAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(Person), typeof(DeletePersonByInternalIdCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}