using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Answers.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByInternalIdCommandHandler : BaseDeleteByInternalIdCommandHandler<Answer, AnswerDto>
{
    public DeleteAnswerByInternalIdCommandHandler(IRepository<Answer> repository, ILogger logger)
        : base(repository, logger)
    {
    }
}