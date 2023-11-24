using Domain.Enums;

namespace Application.EntityManagement.Payments.Dtos;

public record PaymentDto
(
    int OrderExternalId,
    int UserExternalId,
    decimal Amount,
    PaymentMethod PaymentMethod,
    PaymentStatus PaymentStatus
);