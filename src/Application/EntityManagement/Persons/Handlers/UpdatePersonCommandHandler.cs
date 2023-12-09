using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Persons.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Persons.Handlers;

public class UpdatePersonCommandHandler(
        IRepository<Person> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdatePersonCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = mappingService.Map(request.PersonDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Person), typeof(UpdatePersonCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Person), typeof(UpdatePersonCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}