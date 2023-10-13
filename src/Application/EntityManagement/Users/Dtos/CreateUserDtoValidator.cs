using Application.Common;
using Application.EntityManagement.PhoneNumbers.Dtos;
using FluentValidation;

namespace Application.EntityManagement.Users.Dtos;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(model => model.Password)
            .NotEmpty()
            .Matches(RegexPatterns.PasswordPattern);

        RuleFor(model => model.Username)
            .NotEmpty()
            .Matches(RegexPatterns.UsernamePattern);

        RuleForEach(model => model.PhoneNumberDtos)
            .SetValidator(new PhoneNumberDtoValidator());

        RuleFor(model => model.Password)
            .Equal(model => model.PasswordConfirmation)
            .NotEmpty()
            .Matches(RegexPatterns.PasswordPattern);

        RuleFor(model => model.AddressDtos)
            .NotEmpty();

        RuleFor(model => model.PhoneNumberDtos)
            .NotEmpty();
    }
}