#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Answers.Handlers;

public class CreateAnswerCommandHandler : IRequestHandler<CreateAnswerCommand, CommandResult>
{
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateAnswerCommandHandler(IRepository<Answer> answerRepository,
        IRepository<Question> questionRepository,
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _answerRepository = answerRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateAnswerCommand request, CancellationToken cancellationToken)
    {
        var question = await _questionRepository.GetByExternalIdAsync(request.CreateAnswerDto.QuestionExternalId, cancellationToken);

        if (question is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var user = await _userRepository.GetByExternalIdAsync(request.CreateAnswerDto.UserExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(Messages.BadRequest);
        }

        var entity = _mappingService.Map<CreateAnswerDto, Answer>(request.CreateAnswerDto);

        if (entity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Answer), typeof(CreateAnswerCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        entity.QuestionId = question.InternalId;
        entity.UserId = user.InternalId;

        var createdEntity = await _answerRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        _logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Answer), typeof(CreateAnswerCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}