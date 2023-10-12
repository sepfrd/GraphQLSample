using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Users.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Users.Handlers;

public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, CommandResult>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMappingService _mappingService;
    private readonly ILogger _logger;

    public UpdateUserCommandHandler(IUnitOfWork unitOfWork, IMappingService mappingService, ILogger logger)
    {
        _unitOfWork = unitOfWork;
        _mappingService = mappingService;
        _logger = logger;
    }

    public async Task<CommandResult> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
    {
        var user = await _unitOfWork.UserRepository.GetByExternalIdAsync(request.UserDto.Id, null, cancellationToken);

        if (user is null)
        {
            return CommandResult.Success(Messages.NotFound);
        }

        var newEntity = _mappingService.Map(request.UserDto, user);

        if (newEntity is null)
        {
            _logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(User), typeof(UpdateUserCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var updatedEntity = await _unitOfWork.UserRepository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return CommandResult.Success(Messages.SuccessfullyUpdated);
        }

        _logger.LogError(message: Messages.EntityUpdateFailed, DateTime.UtcNow, typeof(User), typeof(UpdateUserCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}