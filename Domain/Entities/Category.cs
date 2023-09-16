using Domain.Common;

namespace Domain.Entities;

public class Category : BaseEntity
{
    public string? Name { get; set; }
    public string? Description { get; set; }
    public string? Image { get; set; }

    public List<Product>? Products { get; set; }
}