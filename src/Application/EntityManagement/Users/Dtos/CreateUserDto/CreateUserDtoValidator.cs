using Application.Common.Constants;
using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using FluentValidation;

namespace Application.EntityManagement.Users.Dtos.CreateUserDto;

public class CreateUserDtoValidator : AbstractValidator<CreateUserDto>
{
    public CreateUserDtoValidator()
    {
        RuleFor(createUserDto => createUserDto.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.")
            .MaximumLength(50)
            .WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(createUserDto => createUserDto.LastName)
            .NotEmpty().WithMessage("LastName is required.")
            .MaximumLength(50)
            .WithMessage("LastName cannot exceed 50 characters.");

        RuleFor(createUserDto => createUserDto.BirthDate)
            .Must(birthDate => birthDate.AddYears(16) <= DateTime.Today)
            .WithMessage("Age must be at least 16 years");

        RuleFor(createUserDto => createUserDto.Username)
            .NotEmpty()
            .WithMessage("Username is required.")
            .Matches(RegexPatternConstants.UsernamePattern)
            .WithMessage("Username requirements not met.");

        RuleFor(createUserDto => createUserDto.Password)
            .NotEmpty()
            .WithMessage("Password is required.")
            .Matches(RegexPatternConstants.PasswordPattern)
            .WithMessage("Password requirements not met.");

        RuleFor(createUserDto => createUserDto.PasswordConfirmation)
            .Equal(dto => dto.Password)
            .WithMessage("Password and PasswordConfirmation must match.");

        RuleFor(createUserDto => createUserDto.Email)
            .NotEmpty()
            .WithMessage("Email is required.")
            .EmailAddress()
            .WithMessage("Invalid Email.");

        RuleForEach(createUserDto => createUserDto.Addresses)
            .SetValidator(new AddressDtoValidator());

        RuleForEach(createUserDto => createUserDto.PhoneNumbers)
            .SetValidator(new PhoneNumberDtoValidator());
    }
}