using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Handlers;

public class GetAllAnswersQueryHandler : BaseGetAllQueryHandler<Answer>
{
    public GetAllAnswersQueryHandler(IRepository<Answer> repository)
        : base(repository)
    {
    }
}