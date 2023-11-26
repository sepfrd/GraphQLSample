#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Addresses.Handlers;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CommandResult>
{
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateAddressCommandHandler(IRepository<Address> addressRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.CreateAddressDto.UserExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var entity = _mappingService.Map<CreateAddressDto, Address>(request.CreateAddressDto);

        if (entity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Address), typeof(CreateAddressCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        entity.UserId = user.InternalId;

        var createdEntity = await _addressRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Address), typeof(CreateAddressCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}