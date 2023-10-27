using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class DeleteUserByInternalIdCommandHandler : IRequestHandler<DeleteUserByInternalIdCommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly ILogger _logger;

    public DeleteUserByInternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(DeleteUserByInternalIdCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork
            .UserRepository
            .GetByInternalIdAsync(request.InternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var deletedUser = await _unitOfWork.UserRepository.DeleteAsync(user, cancellationToken);

        if (deletedUser is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyDeleted);
        }

        _logger.LogError(Messages.EntityDeletionFailed, DateTime.UtcNow, typeof(User), typeof(DeleteUserByInternalIdCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}