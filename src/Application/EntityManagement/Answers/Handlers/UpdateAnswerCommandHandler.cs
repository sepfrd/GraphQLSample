using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class UpdateAnswerCommandHandler : IRequestHandler<UpdateAnswerCommand, CommandResult>
{
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public UpdateAnswerCommandHandler(
        IRepository<Answer> answerRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger,
        IAuthenticationService authenticationService)
    {
        _answerRepository = answerRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _logger = logger;
        _authenticationService = authenticationService;
    }

    public virtual async Task<CommandResult> Handle(UpdateAnswerCommand request, CancellationToken cancellationToken)
    {
        var entity = await _answerRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(UpdateAnswerCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(UpdateAnswerCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var newEntity = _mappingService.Map(request.AnswerDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Answer), typeof(UpdateAnswerCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _answerRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Answer), typeof(UpdateAnswerCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}