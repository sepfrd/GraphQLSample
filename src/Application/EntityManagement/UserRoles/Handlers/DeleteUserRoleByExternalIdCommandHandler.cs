using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.UserRoles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class DeleteUserRoleByExternalIdCommandHandler : IRequestHandler<DeleteUserRoleByExternalIdCommand, CommandResult>
{
    private readonly IRepository<UserRole> _repository;
    private readonly ILogger _logger;

    public DeleteUserRoleByExternalIdCommandHandler(IRepository<UserRole> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteUserRoleByExternalIdCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var deletedEntity = await _repository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(UserRole), typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}