using Application.Abstractions;
using Application.Common;
using Application.Common.Constants;
using Application.EntityManagement.Answers.Commands;
using Application.EntityManagement.Orders.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Orders.Handlers;

public class DeleteOrderByExternalIdCommandHandler : IRequestHandler<DeleteOrderByExternalIdCommand, CommandResult>
{
    private readonly IRepository<Order> _orderRepository;
    private readonly IRepository<User> _userRepository;
    private readonly IAuthenticationService _authenticationService;
    private readonly ILogger _logger;

    public DeleteOrderByExternalIdCommandHandler(
        IRepository<Order> orderRepository,
        IRepository<User> userRepository,
        IAuthenticationService authenticationService,
        ILogger logger)
    {
        _orderRepository = orderRepository;
        _userRepository = userRepository;
        _authenticationService = authenticationService;
        _logger = logger;
    }

    public virtual async Task<CommandResult> Handle(DeleteOrderByExternalIdCommand request,
        CancellationToken cancellationToken)
    {
        var entity = await _orderRepository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Failure(MessageConstants.NotFound);
        }

        var userClaims = _authenticationService.GetLoggedInUser();

        if (userClaims?.ExternalId is null)
        {
            _logger.LogError(message: MessageConstants.ClaimsRetrievalFailed, DateTime.UtcNow,
                typeof(DeleteOrderByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var userExternalId = (int)userClaims.ExternalId;

        var user = await _userRepository.GetByExternalIdAsync(userExternalId, cancellationToken);

        if (user is null)
        {
            _logger.LogError(message: MessageConstants.EntityRetrievalFailed, DateTime.UtcNow, typeof(User),
                typeof(DeleteOrderByExternalIdCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        if (entity.UserId != user.InternalId)
        {
            return CommandResult.Failure(MessageConstants.Forbidden);
        }

        var deletedEntity = await _orderRepository.DeleteOneAsync(entity, cancellationToken);

        if (deletedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyDeleted);
        }

        _logger.LogError(MessageConstants.EntityDeletionFailed, DateTime.UtcNow, typeof(Order),
            typeof(DeleteAnswerByExternalIdCommand));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}