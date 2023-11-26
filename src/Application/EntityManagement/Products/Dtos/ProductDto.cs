#region

using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Comments.Dtos;

#endregion

namespace Application.EntityManagement.Products.Dtos;

public record ProductDto(
    int ExternalId,
    string? Name,
    string? Description,
    decimal Price,
    int StockQuantity,
    IEnumerable<string>? ImageUrls,
    CategoryDto Category,
    IEnumerable<CommentDto> Comments);