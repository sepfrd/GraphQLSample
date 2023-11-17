using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Votes.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class CreateVoteCommandHandler(
        IRepository<Vote> voteRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseCreateCommandHandler<Vote, VoteDto>(voteRepository, mappingService, logger);