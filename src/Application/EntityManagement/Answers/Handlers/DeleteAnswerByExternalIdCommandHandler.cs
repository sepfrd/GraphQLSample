using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Answers.Handlers;

public class DeleteAnswerByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Answer>
{
    public DeleteAnswerByExternalIdCommandHandler(IRepository<Answer> repository, ILogger logger)
        : base(repository, logger)
    {
    }
}