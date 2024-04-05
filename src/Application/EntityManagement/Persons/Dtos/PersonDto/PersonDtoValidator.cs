using FluentValidation;

namespace Application.EntityManagement.Persons.Dtos.PersonDto;

public class PersonDtoValidator : AbstractValidator<PersonDto>
{
    public PersonDtoValidator()
    {
        RuleFor(personDto => personDto.FirstName)
            .NotEmpty()
            .WithMessage("FirstName is required.")
            .MaximumLength(50)
            .WithMessage("FirstName cannot exceed 50 characters.");

        RuleFor(personDto => personDto.LastName)
            .NotEmpty()
            .WithMessage("LastName is required.")
            .MaximumLength(50)
            .WithMessage("LastName cannot exceed 50 characters.");

        RuleFor(personDto => personDto.BirthDate)
            .Must(birthDate => birthDate.AddYears(16) <= DateTime.Today)
            .WithMessage("Age must be at least 16 years");
    }
}