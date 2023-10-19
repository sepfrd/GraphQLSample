using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class DeletePaymentByExternalIdCommandHandler : BaseDeleteByExternalIdCommandHandler<Payment>
{
    public DeletePaymentByExternalIdCommandHandler(IUnitOfWork unitOfWork, ILogger logger) : base(unitOfWork, logger)
    {
    }
}