using Application.Common;
using Application.Common.Constants;
using FluentValidation;

namespace Application.EntityManagement.PhoneNumbers.Dtos;

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(model => model.Number)
            .NotEmpty()
            .Matches(RegexPatternConstants.PhoneNumberPattern);

        RuleFor(model => model.Type)
            .IsInEnum();
    }
}