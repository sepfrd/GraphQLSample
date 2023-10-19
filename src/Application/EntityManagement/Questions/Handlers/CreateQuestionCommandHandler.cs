using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Questions.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Questions.Handlers;

public class CreateQuestionCommandHandler : BaseCreateCommandHandler<Question, QuestionDto>
{
    public CreateQuestionCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}