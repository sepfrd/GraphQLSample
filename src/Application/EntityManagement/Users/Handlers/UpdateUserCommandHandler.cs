using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CommandResult>
{
    private readonly IRepository<User> _userRepository;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateUserCommandHandler(
        IRepository<User> userRepository,
        IMappingService mappingService,
        ILogger logger)
    {
        _userRepository = userRepository;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByExternalIdAsync(request.UserDto.ExternalId, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = _mappingService.Map(request.UserDto, user);

        if (newEntity is null)
        {
            _logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(User),
                typeof(UpdateUserCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await _userRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        _logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(User),
            typeof(UpdateUserCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}