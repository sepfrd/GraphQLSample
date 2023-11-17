using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class DeleteVoteByExternalIdCommandHandler(
        IRepository<Vote> voteRepository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Vote>(voteRepository, logger);