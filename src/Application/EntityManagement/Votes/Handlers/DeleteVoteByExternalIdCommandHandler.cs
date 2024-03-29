using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Votes.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class DeleteVoteByExternalIdCommandHandler : IRequestHandler<DeleteVoteByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Vote> _voteRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public DeleteVoteByExternalIdCommandHandler(
        IRepository<Vote> voteRepository,
        IRepository<User> userRepository,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _voteRepository = voteRepository;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteVoteByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _voteRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(DeleteVoteByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(DeleteVoteByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var deletedEntity = await _voteRepository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(Vote), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}