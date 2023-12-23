using Domain.Enums;

namespace Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;

public record PhoneNumberDto(string Number, PhoneNumberType Type);