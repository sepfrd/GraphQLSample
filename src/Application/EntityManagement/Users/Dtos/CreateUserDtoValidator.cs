using Application.Common.Constants;
using Application.EntityManagement.PhoneNumbers.Dtos;
using FluentValidation;

namespace Application.EntityManagement.Users.Dtos;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(model => model.Password)
            .NotEmpty()
            .Matches(RegexPatternConstants.PasswordPattern);

        RuleFor(model => model.Username)
            .NotEmpty()
            .Matches(RegexPatternConstants.UsernamePattern);

        RuleForEach(model => model.PhoneNumbers)
            .SetValidator(new PhoneNumberDtoValidator());

        RuleFor(model => model.Password)
            .Equal(model => model.PasswordConfirmation)
            .NotEmpty()
            .Matches(RegexPatternConstants.PasswordPattern);

        RuleFor(model => model.Addresses)
            .NotEmpty();

        RuleFor(model => model.PhoneNumbers)
            .NotEmpty();
    }
}