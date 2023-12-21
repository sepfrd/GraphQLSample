using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Addresses.Commands;
using Application.EntityManagement.Answers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Addresses.Handlers;

public class DeleteAddressByExternalIdCommandHandler : IRequestHandler<DeleteAddressByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Address> _addressRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public DeleteAddressByExternalIdCommandHandler(
        IRepository<Address> addressRepository,
        IRepository<User> userRepository,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _addressRepository = addressRepository;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteAddressByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _addressRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(DeleteAddressByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(DeleteAddressByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var deletedEntity = await _addressRepository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(Address), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}