using Domain.Enums;

namespace Application.EntityManagement.Payments.Dtos;

public record CreatePaymentDto(
    decimal Amount,
    PaymentMethod PaymentMethod,
    PaymentStatus PaymentStatus);