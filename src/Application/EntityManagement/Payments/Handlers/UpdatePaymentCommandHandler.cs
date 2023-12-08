using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Payments.Commands;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class UpdatePaymentCommandHandler(
        IRepository<Payment> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<UpdatePaymentCommand, CommandResult>
{
    public virtual async Task<CommandResult> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = await repository.GetByExternalIdAsync(request.ExternalId, cancellationToken);

        if (entity is null)
        {
            return CommandResult.Success(MessageConstants.NotFound);
        }

        var newEntity = mappingService.Map(request.CreatePaymentDto, entity);

        if (newEntity is null)
        {
            logger.LogError(message: MessageConstants.MappingFailed, DateTime.UtcNow, typeof(Payment), typeof(UpdatePaymentCommandHandler));

            return CommandResult.Failure(MessageConstants.InternalServerError);
        }

        var updatedEntity = await repository.UpdateAsync(newEntity, cancellationToken);

        if (updatedEntity is not null)
        {
            return CommandResult.Success(MessageConstants.SuccessfullyUpdated);
        }

        logger.LogError(message: MessageConstants.EntityUpdateFailed, DateTime.UtcNow, typeof(Payment), typeof(UpdatePaymentCommandHandler));

        return CommandResult.Failure(MessageConstants.InternalServerError);
    }
}