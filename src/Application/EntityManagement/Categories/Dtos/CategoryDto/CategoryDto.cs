namespace Application.EntityManagement.Categories.Dtos.CategoryDto;

public record CategoryDto(
    string Name,
    string Description,
    string ImageUrl,
    string IconUrl);