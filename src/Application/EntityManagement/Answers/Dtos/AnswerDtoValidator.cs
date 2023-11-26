using FluentValidation;

namespace Application.EntityManagement.Answers.Dtos;

public class AnswerDtoValidator : AbstractValidator<AnswerDto>
{
    public AnswerDtoValidator()
    {
        RuleFor(model => model.Title)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(model => model.Description)
            .NotEmpty()
            .MinimumLength(20);
    }
}