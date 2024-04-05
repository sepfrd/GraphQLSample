using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.UserRoles.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.UserRoles.Handlers;

public class CreateUserRoleCommandHandler : IRequestHandler<CreateUserRoleCommand, CommandResult>
{
    private readonly IRepository<UserRole> _userRoleRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IRepository<Role> _roleRepository;
    private readonly ILogger _logger;

    public CreateUserRoleCommandHandler(
        IRepository<UserRole> userRoleRepository,
        IRepository<Role> roleRepository,
        IRepository<User> userRepository,
        ILogger logger)
    {
        _userRoleRepository = userRoleRepository;
        _roleRepository = roleRepository;
        _userRepository = userRepository;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(CreateUserRoleCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.UserRoleDto.UserExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var role = await _roleRepository.GetByExternalIdAsync(request.UserRoleDto.RoleExternalId, cancellationToken);

        if (role is null)
        {
            return CommandResult.Failure(MessageConstants.BadRequest);
        }

        var entity = new UserRole
        {
            UserId = user.InternalId,
            RoleId = role.InternalId
        };

        var createdEntity = await _userRoleRepository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyCreated);
        }

        _logger.LogError(message: MessageConstants.EntityCreationFailed, DateTime.UtcNow, typeof(UserRole),
            typeof(CreateUserRoleCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}