using FluentValidation;

namespace Application.EntityManagement.Comments.Dtos.CommentDto;

public class CommentDtoValidator : AbstractValidator<CommentDto>
{
    public CommentDtoValidator()
    {
        RuleFor(commentDto => commentDto.ProductExternalId)
            .GreaterThan(0)
            .WithMessage("ProductExternalId should be greater than 0.");

        RuleFor(commentDto => commentDto.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");
    }
}