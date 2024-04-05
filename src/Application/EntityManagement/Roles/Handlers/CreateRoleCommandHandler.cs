using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Roles.Commands;
using Application.EntityManagement.Roles.Dtos.RoleDto;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Roles.Handlers;

public class CreateRoleCommandHandler : IRequestHandler<CreateRoleCommand, CommandResult>
{
    private readonly IRepository<Role> _repository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public CreateRoleCommandHandler(
        IRepository<Role> repository,
        IMappingService mappingService,
        ILogger logger)
    {
        _repository = repository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = _mappingService.Map<RoleDto, Role>(request.RoleDto);

        if (entity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Role),
                typeof(CreateRoleCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var createdEntity = await _repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(Role),
            typeof(CreateRoleCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}