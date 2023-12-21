using Domain.Enums;

namespace Application.EntityManagement.Payments.Dtos;

public record PaymentDto(
    int OrderExternalId,
    decimal Amount,
    PaymentMethod PaymentMethod,
    PaymentStatus PaymentStatus);