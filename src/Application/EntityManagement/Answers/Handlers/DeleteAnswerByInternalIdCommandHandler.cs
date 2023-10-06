using Application.Common.Handlers;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByInternalIdCommandHandler : BaseDeleteByInternalIdCommandHandler<Answer, AnswerDto>
{
    public DeleteAnswerByInternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
        : base(unitOfWork, logger)
    {
    }
}