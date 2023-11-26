using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Votes.Commands;
using Application.EntityManagement.Votes.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Votes.Handlers;

public class CreateVoteCommandHandler(
        IRepository<Vote> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateVoteCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateVoteCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<VoteDto, Vote>(request.VoteDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Vote), typeof(CreateVoteCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Vote), typeof(CreateVoteCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}