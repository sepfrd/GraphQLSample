#region

using Application.Abstractions;
using Application.Common;
using Application.EntityManagement.Payments.Commands;
using Application.EntityManagement.Payments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;

#endregion

namespace Application.EntityManagement.Payments.Handlers;

public class CreatePaymentCommandHandler(
        IRepository<Payment> repository,
        IMappingService mappingService,
        ILogger logger)
    : IRequestHandler<CreatePaymentCommand, CommandResult>
{
    public async Task<CommandResult> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var entity = mappingService.Map<CreatePaymentDto, Payment>(request.CreatePaymentDto);

        if (entity is null)
        {
            logger.LogError(message: Messages.MappingFailed, DateTime.UtcNow, typeof(Payment), typeof(CreatePaymentCommandHandler));

            return CommandResult.Failure(Messages.InternalServerError);
        }

        var createdEntity = await repository.CreateAsync(entity, cancellationToken);

        if (createdEntity is not null)
        {
            return CommandResult.Success(Messages.SuccessfullyCreated);
        }

        logger.LogError(message: Messages.EntityCreationFailed, DateTime.UtcNow, typeof(Payment), typeof(CreatePaymentCommandHandler));

        return CommandResult.Failure(Messages.InternalServerError);
    }
}