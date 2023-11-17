using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class UpdateAnswerCommandHandler(
        IRepository<Answer> answerRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Answer, AnswerDto>(answerRepository, mappingService, logger);