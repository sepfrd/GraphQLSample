using FluentValidation;

namespace Application.EntityManagement.Answers.Commands.UpdateAnswer;

public class UpdateAnswerCommandValidator : AbstractValidator<UpdateAnswerCommand>
{
    public UpdateAnswerCommandValidator()
    {
        RuleFor(dto => dto.UpdateAnswerDto.Title)
            .NotEmpty()
            .MinimumLength(5);

        RuleFor(dto => dto.UpdateAnswerDto.Description)
            .NotEmpty()
            .MinimumLength(20);
    }
}