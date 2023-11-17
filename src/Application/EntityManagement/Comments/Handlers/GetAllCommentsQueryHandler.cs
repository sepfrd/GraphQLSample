using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Comments.Handlers;

public class GetAllCommentsQueryHandler(IRepository<Comment> commentRepository)
    : BaseGetAllQueryHandler<Comment>(commentRepository);