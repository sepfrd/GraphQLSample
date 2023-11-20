using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class CreateAddressCommandHandler(
        IRepository<Address> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateAddressCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<AddressDto, Address>(request.AddressDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Address), typeof(CreateAddressCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Address), typeof(CreateAddressCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}