using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.UserRoles.Commands;
using Application.EntityManagement.UserRoles.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class CreateUserRoleCommandHandler(
        IRepository<UserRole> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreateUserRoleCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<UserRoleDto, UserRole>(request.UserRoleDto);

        if (entity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(UserRole), typeof(CreateUserRoleCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(UserRole), typeof(CreateUserRoleCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}