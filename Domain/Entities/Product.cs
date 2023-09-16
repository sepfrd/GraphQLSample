using Domain.Common;

namespace Domain.Entities;

public class Product : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public decimal Price { get; set; }
    public int StockQuantity { get; set; }
    public List<string>? Images { get; set; }

    public Category? Category { get; set; }
}
