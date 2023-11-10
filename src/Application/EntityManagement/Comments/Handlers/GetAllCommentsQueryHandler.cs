using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Comments.Handlers;

public class GetAllCommentsQueryHandler : BaseGetAllQueryHandler<Comment>
{
    public GetAllCommentsQueryHandler(IUnitOfWork unitOfWork) : base(unitOfWork)
    {
    }
}