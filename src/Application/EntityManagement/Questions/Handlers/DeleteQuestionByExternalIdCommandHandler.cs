using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Questions.Handlers;

public class DeleteQuestionByExternalIdCommandHandler(IRepository<Question> questionRepository, ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Question>(questionRepository, logger);