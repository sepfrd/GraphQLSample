using FluentValidation;

namespace Application.EntityManagement.Answers.Dtos.AnswerDto;

public class AnswerDtoValidator : AbstractValidator<AnswerDto>
{
    public AnswerDtoValidator()
    {
        RuleFor(answerDto => answerDto.Title)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(answerDto => answerDto.Description)
            .NotEmpty()
            .MinimumLength(20);
    }
}