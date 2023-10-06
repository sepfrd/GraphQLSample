using Domain.Abstractions;
using Domain.Common;

namespace Domain.Entities;

public sealed class Product : BaseEntity, IVotableContent
{
    public string? Name { get; set; }

    public string? Description { get; set; }

    public decimal Price { get; set; }

    public int StockQuantity { get; set; }

    public ICollection<string>? ImageUrls { get; set; }

    public Guid CategoryId { get; set; }

    public ICollection<Guid>? CommentIds { get; set; }

    public ICollection<Guid>? VoteIds { get; set; }
}