using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class CreateAnswerCommandHandler(
        IRepository<Answer> answerRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Answer, AnswerDto>(answerRepository, mappingService, logger);