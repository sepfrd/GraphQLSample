using Application.Common.Constants;
using FluentValidation;

namespace Application.EntityManagement.Categories.Dtos.CategoryDto;

public class CategoryDtoValidator : AbstractValidator<CategoryDto>
{
    public CategoryDtoValidator()
    {
        RuleFor(categoryDto => categoryDto.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(categoryDto => categoryDto.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(categoryDto => categoryDto.ImageUrl)
            .NotEmpty()
            .WithMessage("ImageUrl is required.")
            .Matches(RegexPatternConstants.UrlPattern)
            .WithMessage("ImageUrl should be a valid URL.");

        RuleFor(categoryDto => categoryDto.IconUrl)
            .Matches(RegexPatternConstants.UrlPattern)
            .WithMessage("IconUrl should be a valid URL.");
    }
}