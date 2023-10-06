using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Handlers;

public class GetAnswerByInternalIdQueryHandler : BaseGetByInternalIdQueryHandler<Answer>
{
    public GetAnswerByInternalIdQueryHandler(IUnitOfWork unitOfWork)
        : base(unitOfWork)
    {
    }
}