using FluentValidation;

namespace Application.EntityManagement.Questions.Dtos.QuestionDto;

public class QuestionDtoValidator : AbstractValidator<QuestionDto>
{
    public QuestionDtoValidator()
    {
        RuleFor(questionDto => questionDto.Title)
            .NotEmpty()
            .WithMessage("Title is required.")
            .MaximumLength(100)
            .WithMessage("Title cannot exceed 100 characters.");

        RuleFor(questionDto => questionDto.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(1000)
            .WithMessage("Description cannot exceed 1000 characters.");
    }
}