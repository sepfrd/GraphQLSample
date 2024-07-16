using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Roles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class UpdateRoleCommandHandler : IRequestHandler<UpdateRoleCommand, CommandResult>
{
    private readonly IRepository<Role> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateRoleCommandHandler(
        IRepository<Role> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = await _repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = _mappingService.Map(request.RoleDto, entity);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Role),
                typeof(UpdateRoleCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Role),
            typeof(UpdateRoleCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}