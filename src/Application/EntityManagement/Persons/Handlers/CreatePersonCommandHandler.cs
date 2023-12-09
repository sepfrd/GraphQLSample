using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Persons.Commands;
using Application.EntityManagement.Persons.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

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
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Person), typeof(CreatePersonCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Person), typeof(CreatePersonCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}