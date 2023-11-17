using Application.Abstractions;
using Application.Common.Handlers;
using Application.EntityManagement.Payments.Dtos;
using Domain.Abstractions;
using Domain.Entities;
using Microsoft.Extensions.Logging;

namespace Application.EntityManagement.Payments.Handlers;

public class UpdatePaymentCommandHandler(
        IRepository<Payment> paymentRepository,
        IMappingService mappingService,
        ILogger logger)
    : BaseUpdateCommandHandler<Payment, PaymentDto>(paymentRepository, mappingService, logger);