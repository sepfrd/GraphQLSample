using FluentValidation;
using Infrastructure.Common.Constants;

namespace Infrastructure.Services.AuthService.Dtos.SignupDto;

public class SignupDtoValidator : AbstractValidator<SignupDto>
{
    public SignupDtoValidator()
    {
        RuleFor(signupDto => signupDto.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Matches(RegexPatternConstants.UsernamePattern)
            .WithMessage("Username requirements not met.");

        RuleFor(signupDto => signupDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(RegexPatternConstants.PasswordPattern)
            .WithMessage("Password requirements not met.");

        RuleFor(signupDto => signupDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid Email.");
    }
}