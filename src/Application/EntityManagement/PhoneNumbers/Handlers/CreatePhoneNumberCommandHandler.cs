using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.PhoneNumbers.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class CreatePhoneNumberCommandHandler : IRequestHandler<CreatePhoneNumberCommand, CommandResult>
{
    private readonly IRepository<PhoneNumber> _phoneNumberRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreatePhoneNumberCommandHandler(
        IRepository<PhoneNumber> phoneNumberRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _phoneNumberRepository = phoneNumberRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreatePhoneNumberCommand request, CancellationToken cancellationToken)
    {
        var entity = _mappingService.Map<PhoneNumberDto, PhoneNumber>(request.PhoneNumberDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(PhoneNumber), typeof(CreatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(CreatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(CreatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.UserId = user.InternalId;

        var createdEntity = await _phoneNumberRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(PhoneNumber), typeof(CreatePhoneNumberCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}