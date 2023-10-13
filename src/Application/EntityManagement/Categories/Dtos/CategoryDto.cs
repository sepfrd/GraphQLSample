namespace Application.EntityManagement.Categories.Dtos;

public record CategoryDto(int ExternalId, string? Name, string? Description, string? ImageUrl, string? IconUrl);