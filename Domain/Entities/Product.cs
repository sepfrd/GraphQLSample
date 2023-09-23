using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<string> ImageUrls { get; set; } = new();

    public Guid CategoryId { get; set; }
    public ICollection<Guid>? CommentIds { get; set; }
    public ICollection<Guid>? ReviewIds { get; set; }
}
