using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class DeletePaymentByExternalIdCommandHandler(
        IRepository<Payment> paymentRepository,
        ILogger logger)
    : BaseDeleteByExternalIdCommandHandler<Payment>(paymentRepository, logger);