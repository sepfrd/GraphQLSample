namespace Application.EntityManagement.Products.Dtos.ProductDto;

public record ProductDto(
    string? Name,
    string? Description,
    decimal Price,
    int StockQuantity,
    IEnumerable<string>? ImageUrls,
    int CategoryExternalId);