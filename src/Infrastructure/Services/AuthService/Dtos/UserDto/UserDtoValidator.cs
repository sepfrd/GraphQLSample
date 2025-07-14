using FluentValidation;
using Infrastructure.Common.Constants;

namespace Infrastructure.Services.AuthService.Dtos.UserDto;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(userDto => userDto.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Matches(RegexPatternConstants.UsernamePattern)
            .WithMessage("Username requirements not met.");

        RuleFor(userDto => userDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid Email.");
    }
}