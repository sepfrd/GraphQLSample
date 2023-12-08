using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class UpdateAddressCommandHandler : IRequestHandler<UpdateAddressCommand, CommandResult>
{
    private readonly IRepository<Address> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateAddressCommandHandler(IRepository<Address> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdateAddressCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = _mappingService.Map(request.AddressDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Address), typeof(UpdateAddressCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Address), typeof(UpdateAddressCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}