using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Comments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Comments.Handlers;

public class CreateCommentCommandHandler : BaseCreateCommandHandler<Comment, CommentDto>
{
    public CreateCommentCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}