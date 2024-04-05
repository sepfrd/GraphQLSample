using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Roles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class DeleteRoleByExternalIdCommandHandler : IRequestHandler<DeleteRoleByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Role> _repository;
    private readonly ILogger _logger;

    public DeleteRoleByExternalIdCommandHandler(IRepository<Role> repository, ILogger logger)
    {
        _repository = repository;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteRoleByExternalIdCommand request,
        CancellationToken cancellationToken)
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

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(Role),
            typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}