using Application.Common;
using Domain.Entities;

namespace Application.EntityManagement.Comments.Events;

public record CommentDeletedEvent(Comment Entity) : EntityDeletedEvent<Comment>(Entity);