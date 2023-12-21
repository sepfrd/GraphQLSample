using Domain.Enums;

namespace Application.EntityManagement.PhoneNumbers.Dtos;

public record PhoneNumberDto(string Number, PhoneNumberType Type);