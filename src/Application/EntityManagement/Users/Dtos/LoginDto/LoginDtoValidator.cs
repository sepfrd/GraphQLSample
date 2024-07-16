using Application.Common.Constants;
using FluentValidation;

namespace Application.EntityManagement.Users.Dtos.LoginDto;

public class LoginDtoValidator : AbstractValidator<LoginDto>
{
    public LoginDtoValidator()
    {
        RuleFor(loginDto => loginDto.UsernameOrEmail)
            .NotEmpty()
            .WithMessage("UsernameOrEmail is required.");

        RuleFor(loginDto => loginDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(RegexPatternConstants.PasswordPattern);
    }
}