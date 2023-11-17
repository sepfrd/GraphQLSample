using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByExternalIdCommandHandler(IRepository<Answer> answerRepository, ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Answer>(answerRepository, logger);