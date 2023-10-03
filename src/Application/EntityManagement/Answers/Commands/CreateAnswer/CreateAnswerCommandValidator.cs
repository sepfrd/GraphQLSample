using FluentValidation;

namespace Application.EntityManagement.Answers.Commands.CreateAnswer;

public class CreateAnswerCommandValidator : AbstractValidator<CreateAnswerCommand>
{
    public CreateAnswerCommandValidator()
    {
        RuleFor(dto => dto.AnswerDto.Title)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(dto => dto.AnswerDto.Description)
            .NotEmpty()
            .MinimumLength(20);
    }
}