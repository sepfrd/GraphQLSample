using Application.Common;
using Domain.Enums;
using FluentValidation;

namespace Application.EntityManagement.PhoneNumbers.Dtos;

public class PhoneNumberDtoValidator : AbstractValidator<PhoneNumberDto>
{
    public PhoneNumberDtoValidator()
    {
        RuleFor(model => model.Number)
            .NotEmpty()
            .Matches(RegexPatterns.PhoneNumberPattern);

        RuleFor(model => model.Type)
            .IsInEnum();
    }
}