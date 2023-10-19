using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class DeleteVoteByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Vote>
{
    public DeleteVoteByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}