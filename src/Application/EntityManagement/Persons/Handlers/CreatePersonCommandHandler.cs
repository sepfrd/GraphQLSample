#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Persons.Commands;
using Application.EntityManagement.Persons.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Persons.Handlers;

public class CreatePersonCommandHandler(
        IRepository<Person> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreatePersonCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreatePersonCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<PersonDto, Person>(request.PersonDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Person), typeof(CreatePersonCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Person), typeof(CreatePersonCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}