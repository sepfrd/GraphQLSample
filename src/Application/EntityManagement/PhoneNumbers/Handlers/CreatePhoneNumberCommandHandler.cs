using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.PhoneNumbers.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class CreatePhoneNumberCommandHandler(
        IRepository<PhoneNumber> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreatePhoneNumberCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<PhoneNumberDto, PhoneNumber>(request.PhoneNumberDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(PhoneNumber), typeof(CreatePhoneNumberCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(PhoneNumber), typeof(CreatePhoneNumberCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}