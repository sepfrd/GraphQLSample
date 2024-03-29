using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Questions.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Questions.Handlers;

public class DeleteQuestionByExternalIdCommandHandler : IRequestHandler<DeleteQuestionByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public DeleteQuestionByExternalIdCommandHandler(
        IRepository<Question> questionRepository,
        IRepository<User> userRepository,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteQuestionByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _questionRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(DeleteQuestionByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(DeleteQuestionByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var deletedEntity = await _questionRepository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(Question), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}