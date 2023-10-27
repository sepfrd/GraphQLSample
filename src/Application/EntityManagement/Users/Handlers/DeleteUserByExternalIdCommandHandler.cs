using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class DeleteUserByExternalIdCommandHandler : IRequestHandler<DeleteUserByExternalIdCommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public DeleteUserByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(DeleteUserByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var deletedUser = await _unitOfWork.UserRepository.DeleteAsync(user, cancellationToken);

        if (deletedUser is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(User), typeof(DeleteUserByExternalIdCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}