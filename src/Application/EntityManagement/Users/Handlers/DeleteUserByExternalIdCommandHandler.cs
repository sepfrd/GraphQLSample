using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class DeleteUserByExternalIdCommandHandler(IRepository<User> userRepository, ILogger logger)
    : IRequestHandler<DeleteUserByExternalIdCommand, CommandResult>
{
    public async Task<CommandResult> Handle(DeleteUserByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var deletedUser = await userRepository.DeleteOneAsync(user, cancellationToken);

        if (deletedUser is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(User), typeof(DeleteUserByExternalIdCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}