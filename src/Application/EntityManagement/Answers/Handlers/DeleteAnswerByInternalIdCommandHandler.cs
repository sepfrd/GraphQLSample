using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByInternalIdCommandHandler : BaseDeleteByInternalIdCommandHandler<Answer>
{
    public DeleteAnswerByInternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
        : base(unitOfWork, logger)
    {
    }
}