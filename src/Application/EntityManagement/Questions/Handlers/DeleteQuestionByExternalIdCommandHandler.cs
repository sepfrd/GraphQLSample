using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Questions.Handlers;

public class DeleteQuestionByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Question>
{
    public DeleteQuestionByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}