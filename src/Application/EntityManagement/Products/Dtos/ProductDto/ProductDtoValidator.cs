using Application.Common.Constants;
using FluentValidation;

namespace Application.EntityManagement.Products.Dtos.ProductDto;

public class ProductDtoValidator : AbstractValidator<ProductDto>
{
    public ProductDtoValidator()
    {
        RuleFor(productDto => productDto.Name)
            .NotEmpty()
            .WithMessage("Name is required.")
            .MaximumLength(100)
            .WithMessage("Name cannot exceed 100 characters.");

        RuleFor(productDto => productDto.Description)
            .NotEmpty()
            .WithMessage("Description is required.")
            .MaximumLength(500)
            .WithMessage("Description cannot exceed 500 characters.");

        RuleFor(productDto => productDto.Price)
            .GreaterThan(0)
            .WithMessage("Price should be greater than 0.");

        RuleFor(productDto => productDto.StockQuantity)
            .GreaterThanOrEqualTo(0)
            .WithMessage("StockQuantity should be greater than or equal to 0.");

        RuleFor(productDto => productDto.ImageUrls)
            .NotEmpty()
            .WithMessage("ImageUrls should contain at least one URL.");

        RuleForEach(productDto => productDto.ImageUrls)
            .Matches(RegexPatternConstants.UrlPattern)
            .WithMessage("ImageUrl should be a valid URL.");

        RuleFor(productDto => productDto.CategoryExternalId)
            .GreaterThan(0)
            .WithMessage("CategoryExternalId should be greater than 0.");
    }
}