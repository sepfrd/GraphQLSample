using Application.Common.Commands;
using Application.EntityManagement.PhoneNumbers.Dtos;

namespace Application.EntityManagement.PhoneNumbers.Commands;

public record CreatePhoneNumberCommand(PhoneNumberDto PhoneNumberDto) : BaseCreateCommand<PhoneNumberDto>(PhoneNumberDto);