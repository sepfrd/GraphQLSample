using Application.Common.Commands;
using Application.EntityManagement.Comments.Dtos;

namespace Application.EntityManagement.Comments.Commands;

public record CreateCommentCommand(CommentDto CommentDto) : BaseCreateCommand<CommentDto>(CommentDto);