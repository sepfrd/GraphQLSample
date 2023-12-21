using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Addresses.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class CreateAddressCommandHandler : IRequestHandler<CreateAddressCommand, CommandResult>
{
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreateAddressCommandHandler(IRepository<Address> addressRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateAddressCommand request, CancellationToken cancellationToken)
    {
        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(CreateAddressCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var entity = _mappingService.Map<AddressDto, Address>(request.AddressDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Address), typeof(CreateAddressCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.UserId = user.InternalId;

        var createdEntity = await _addressRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Address), typeof(CreateAddressCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}