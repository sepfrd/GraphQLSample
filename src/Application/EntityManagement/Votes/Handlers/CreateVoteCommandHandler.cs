using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Votes.Commands;
using Application.EntityManagement.Votes.Dtos;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class CreateVoteCommandHandler : IRequestHandler<CreateVoteCommand, CommandResult>
{
    private readonly IRepository<Vote> _voteRepository;
    private readonly IRepository<Answer> _answerRepository;
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IRepository<Question> _questionRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateVoteCommandHandler(
        IRepository<Vote> voteRepository,
        IRepository<Answer> answerRepository,
        IRepository<Comment> commentRepository,
        IRepository<Product> productRepository,
        IRepository<Question> questionRepository,
        IMappingService mappingService,
        ILogger logger, IRepository<User> userRepository)
    {
        _voteRepository = voteRepository;
        _answerRepository = answerRepository;
        _commentRepository = commentRepository;
        _productRepository = productRepository;
        _questionRepository = questionRepository;
        _userRepository = userRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.CreateVoteDto.UserExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var entity = _mappingService.Map<CreateVoteDto, Vote>(request.CreateVoteDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Vote), typeof(CreateVoteCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        BaseEntity? content = entity.ContentType switch
        {
            VotableContentType.Answer => await _answerRepository.GetByExternalIdAsync(request.CreateVoteDto.ContentExternalId, cancellationToken),
            VotableContentType.Comment => await _commentRepository.GetByExternalIdAsync(request.CreateVoteDto.ContentExternalId, cancellationToken),
            VotableContentType.Product => await _productRepository.GetByExternalIdAsync(request.CreateVoteDto.ContentExternalId, cancellationToken),
            VotableContentType.Question => await _questionRepository.GetByExternalIdAsync(request.CreateVoteDto.ContentExternalId, cancellationToken),
            _ => null
        };

        if (content is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        entity.UserId = user.InternalId;
        entity.ContentId = content.InternalId;

        var createdEntity = await _voteRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Vote), typeof(CreateVoteCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}