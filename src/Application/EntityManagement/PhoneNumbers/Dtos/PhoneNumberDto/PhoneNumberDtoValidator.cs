using Application.Common.Constants;
using FluentValidation;

namespace Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(phoneNumberDto => phoneNumberDto.Number)
            .NotEmpty()
            .WithMessage("Number is required.")
            .Matches(RegexPatternConstants.PhoneNumberPattern);

        RuleFor(phoneNumberDto => phoneNumberDto.Type)
            .IsInEnum()
            .WithMessage("Invalid type.");
    }
}