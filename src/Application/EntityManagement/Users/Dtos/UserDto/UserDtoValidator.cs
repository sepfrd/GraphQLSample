using Application.Common.Constants;
using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.Comments.Dtos.CommentDto;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Application.EntityManagement.Persons.Dtos.PersonDto;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using FluentValidation;

namespace Application.EntityManagement.Users.Dtos.UserDto;

public class UserDtoValidator : AbstractValidator<UserDto>
{
    public UserDtoValidator()
    {
        RuleFor(userDto => userDto.ExternalId)
            .GreaterThan(0)
            .WithMessage("ExternalId should be greater than 0.");

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

        RuleFor(userDto => userDto.Score)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Score should be greater than or equal to 0.");

        RuleFor(userDto => userDto.OrdersCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("OrdersCount should be greater than or equal to 0.");

        RuleFor(userDto => userDto.QuestionsCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("QuestionsCount should be greater than or equal to 0.");

        RuleFor(userDto => userDto.AnswersCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("AnswersCount should be greater than or equal to 0.");

        RuleFor(userDto => userDto.VotesCount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("VotesCount should be greater than or equal to 0.");

        RuleFor(userDto => userDto.Person)
            .SetValidator(new PersonDtoValidator()!) // Assuming you have a validator for PersonDto
            .When(dto => dto.Person is not null);

        RuleForEach(userDto => userDto.Addresses)
            .SetValidator(new AddressDtoValidator()) // Assuming you have a validator for AddressDto
            .When(dto => dto.Addresses is not null);

        RuleForEach(dto => dto.PhoneNumbers)
            .SetValidator(new PhoneNumberDtoValidator()) // Assuming you have a validator for PhoneNumberDto
            .When(dto => dto.PhoneNumbers is not null);

        RuleForEach(dto => dto.Payments)
            .SetValidator(new PaymentDtoValidator()) // Assuming you have a validator for PaymentDto
            .When(dto => dto.Payments is not null);

        RuleForEach(dto => dto.Comments)
            .SetValidator(new CommentDtoValidator())
            .When(dto => dto.Comments is not null);
    }
}