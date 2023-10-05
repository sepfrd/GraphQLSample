using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Answers.Handlers;

public class CreateAnswerCommandHandler : BaseCreateCommandHandler<Answer, AnswerDto>
{
    public CreateAnswerCommandHandler(IRepository<Answer> repository, IMappingService mappingService) : base(repository, mappingService)
    {
    }
}