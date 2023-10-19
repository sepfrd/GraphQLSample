using Application.Common.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record UpdatePhoneNumberCommand(int ExternalId, PhoneNumberDto PhoneNumberDto)
    : BaseUpdateCommand<PhoneNumberDto>(ExternalId, PhoneNumberDto);