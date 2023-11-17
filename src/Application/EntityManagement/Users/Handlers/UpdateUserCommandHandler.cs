using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class UpdateUserCommandHandler(
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdateUserCommand, CommandResult>
{
    public async Task<CommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByExternalIdAsync(request.UserDto.ExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = mappingService.Map(request.UserDto, user);

        if (newEntity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(User), typeof(UpdateUserCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await userRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(User), typeof(UpdateUserCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}