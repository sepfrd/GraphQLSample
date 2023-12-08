using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class DeleteUserByInternalIdCommandHandler(IRepository<User> userRepository, ILogger logger)
    : IRequestHandler<DeleteUserByInternalIdCommand, CommandResult>
{
    public async Task<CommandResult> Handle(DeleteUserByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var user = await userRepository.GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var deletedUser = await userRepository.DeleteOneAsync(user, cancellationToken);

        if (deletedUser is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(User), typeof(DeleteUserByInternalIdCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}