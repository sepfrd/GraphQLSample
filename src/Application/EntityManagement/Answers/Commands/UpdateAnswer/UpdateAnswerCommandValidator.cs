using FluentValidation;

namespace Application.EntityManagement.Answers.Commands.UpdateAnswer;

public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommand>
{
    public UpdateAnswerCommandValidator()
    {
        RuleFor(dto => dto.AnswerDto.Title)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(dto => dto.AnswerDto.Description)
            .NotEmpty()
            .MinimumLength(20);
    }
}