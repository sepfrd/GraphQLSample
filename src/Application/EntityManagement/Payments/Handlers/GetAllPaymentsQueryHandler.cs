using Application.Common.Handlers;
using Domain.Abstractions;
using Domain.Entities;

namespace Application.EntityManagement.Payments.Handlers;

public class GetAllPaymentsQueryHandler(IRepository<Payment> paymentRepository)
    : BaseGetAllQueryHandler<Payment>(paymentRepository);