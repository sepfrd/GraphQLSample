using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.PhoneNumbers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.PhoneNumbers.Handlers;

public class UpdatePhoneNumberCommandHandler : IRequestHandler<UpdatePhoneNumberCommand, CommandResult>
{
    private readonly IRepository<PhoneNumber> _repository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public UpdatePhoneNumberCommandHandler(
        IRepository<PhoneNumber> repository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _repository = repository;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdatePhoneNumberCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(UpdatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(UpdatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var newEntity = _mappingService.Map(request.PhoneNumberDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(PhoneNumber),
                typeof(UpdatePhoneNumberCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(PhoneNumber),
            typeof(UpdatePhoneNumberCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}