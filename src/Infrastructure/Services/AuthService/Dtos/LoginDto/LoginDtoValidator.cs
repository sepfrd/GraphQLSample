using FluentValidation;
using Infrastructure.Common.Constants;

namespace Infrastructure.Services.AuthService.Dtos.LoginDto;

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