using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos.CommentDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class CreateCommentCommandHandler : IRequestHandler<CreateCommentCommand, CommandResult>
{
    private readonly IRepository<Comment> _commentRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMappingService _mappingService;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public CreateCommentCommandHandler(
        IRepository<Comment> commentRepository,
        IRepository<User> userRepository,
        IRepository<Product> productRepository,
        IMappingService mappingService,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _mappingService = mappingService;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow, typeof(CreateCommentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User), typeof(CreateCommentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var product = await _productRepository.GetByExternalIdAsync(request.CommentDto.ProductExternalId, cancellationToken);

        if (product is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var entity = _mappingService.Map<CommentDto, Comment>(request.CommentDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Comment), typeof(CreateCommentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        entity.UserId = user.InternalId;
        entity.ProductId = product.InternalId;

        var createdEntity = await _commentRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Comment), typeof(CreateCommentCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}