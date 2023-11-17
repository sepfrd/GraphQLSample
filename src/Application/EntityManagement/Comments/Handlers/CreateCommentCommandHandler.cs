using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Comments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class CreateCommentCommandHandler(
        IRepository<Comment> commentRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Comment, CommentDto>(commentRepository, mappingService, logger);