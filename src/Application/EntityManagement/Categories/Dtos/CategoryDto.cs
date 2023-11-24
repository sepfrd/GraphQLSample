namespace Application.EntityManagement.Categories.Dtos;

public record CategoryDto(
    string? Name,
    string? Description,
    string? ImageUrl,
    string? IconUrl);