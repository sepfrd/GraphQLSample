using Domain.Enums;

namespace Application.EntityManagement.PhoneNumbers.Dtos;

public record PhoneNumberDto(
    int UserExternalId,
    string Number,
    PhoneNumberType Type);