using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, CommandResult>
{
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreateAnswerCommandHandler(
        IRepository<Answer> answerRepository,
        IRepository<Question> questionRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByExternalIdAsync(request.AnswerDto.QuestionExternalId, cancellationToken);

        if (question is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(CreateAnswerCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(CreateAnswerCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }
        
        var entity = _mappingService.Map<AnswerDto, Answer>(request.AnswerDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Answer), typeof(CreateAnswerCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.QuestionId = question.InternalId;
        entity.UserId = user.InternalId;

        var createdEntity = await _answerRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Answer), typeof(CreateAnswerCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}