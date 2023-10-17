using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class DeleteCommentCommandHandler : BaseDeleteByExternalIdCommandHandler<Comment>
{
    public DeleteCommentCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}