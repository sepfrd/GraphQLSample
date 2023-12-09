using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.PhoneNumbers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class UpdatePhoneNumberCommandHandler(
        IRepository<PhoneNumber> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdatePhoneNumberCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = mappingService.Map(request.PhoneNumberDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(PhoneNumber), typeof(UpdatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(PhoneNumber), typeof(UpdatePhoneNumberCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}