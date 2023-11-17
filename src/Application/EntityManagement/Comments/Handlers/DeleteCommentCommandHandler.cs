using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class DeleteCommentCommandHandler(IRepository<Comment> commentRepository, ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Comment>(commentRepository, logger);