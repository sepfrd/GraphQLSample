using Domain.Enums;

namespace Application.EntityManagement.PhoneNumbers.Dtos;

public record PhoneNumberDto(int ExternalId, int UserExternalId, string Number, PhoneNumberType Type);