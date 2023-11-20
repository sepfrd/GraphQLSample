using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class UpdateAddressCommandHandler(
        IRepository<Address> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdateAddressCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = mappingService.Map(request.AddressDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Address), typeof(UpdateAddressCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(Address), typeof(UpdateAddressCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}