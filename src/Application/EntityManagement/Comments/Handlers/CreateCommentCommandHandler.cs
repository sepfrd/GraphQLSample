using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos;
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
    private readonly ILogger _logger;

    public CreateCommentCommandHandler(
        IRepository<Comment> commentRepository,
        IRepository<User> userRepository,
        IRepository<Product> productRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _commentRepository = commentRepository;
        _userRepository = userRepository;
        _productRepository = productRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.CommentDto.UserExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
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