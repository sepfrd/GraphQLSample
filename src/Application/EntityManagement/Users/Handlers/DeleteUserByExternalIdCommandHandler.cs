using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class DeleteUserByExternalIdCommandHandler : IRequestHandler<DeleteUserByExternalIdCommand, CommandResult>
{
    private readonly IRepository<User> _userRepository;
    private readonly ILogger _logger;

    public DeleteUserByExternalIdCommandHandler(IRepository<User> userRepository, ILogger logger)
    {
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(DeleteUserByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var deletedUser = await _userRepository.DeleteOneAsync(user, cancellationToken);

        if (deletedUser is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(User), typeof(DeleteUserByExternalIdCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}