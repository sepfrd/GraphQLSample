using Domain.Common;

namespace Domain.Entities;

public sealed class Category : BaseEntity
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public string? ImageUrl { get; set; }

    public string? IconUrl { get; set; }

    public ICollection<Product>? Products { get; set; }
}