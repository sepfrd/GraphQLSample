using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Questions.Commands;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Questions.Dtos.QuestionDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Questions.Handlers;

public class CreateQuestionCommandHandler : IRequestHandler<CreateQuestionCommand, CommandResult>
{
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreateQuestionCommandHandler(
        IRepository<Question> questionRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateQuestionCommand request, CancellationToken cancellationToken)
    {
        var entity = _mappingService.Map<QuestionDto, Question>(request.QuestionDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Question), typeof(CreateQuestionCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(CreateQuestionCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(CreateQuestionCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.UserId = user.InternalId;

        var createdEntity = await _questionRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Question), typeof(CreateQuestionCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}