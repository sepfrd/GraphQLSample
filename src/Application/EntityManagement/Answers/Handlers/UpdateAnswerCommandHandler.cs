using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class UpdateAnswerCommandHandler : BaseUpdateCommandHandler<Answer, AnswerDto>
{
    public UpdateAnswerCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
        : base(unitOfWork, mappingService, logger)
    {
    }
}