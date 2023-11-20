using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Comments.Commands;
using Application.EntityManagement.Comments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class CreateCommentCommandHandler(
        IRepository<Comment> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateCommentCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateCommentCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<CommentDto, Comment>(request.CommentDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Comment), typeof(CreateCommentCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Comment), typeof(CreateCommentCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}