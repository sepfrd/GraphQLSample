using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Votes.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class CreateVoteCommandHandler : BaseCreateCommandHandler<Vote, VoteDto>
{
    public CreateVoteCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger) : base(unitOfWork, mappingService, logger)
    {
    }
}