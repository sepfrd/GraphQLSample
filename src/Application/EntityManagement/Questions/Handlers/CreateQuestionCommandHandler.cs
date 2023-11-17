using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Questions.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Questions.Handlers;

public class CreateQuestionCommandHandler(
        IRepository<Question> questionRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Question, QuestionDto>(questionRepository, mappingService, logger);