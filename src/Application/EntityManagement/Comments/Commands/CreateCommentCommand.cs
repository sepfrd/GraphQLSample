using Application.Common;
using Application.EntityManagement.Comments.Dtos.CommentDto;
using MediatR;

namespace Application.EntityManagement.Comments.Commands;

public record CreateCommentCommand(CommentDto CommentDto) : IRequest<CommandResult>;