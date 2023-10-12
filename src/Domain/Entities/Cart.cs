using Domain.Common;

namespace Domain.Entities;

public sealed class Cart : BaseEntity
{
    public User? User { get; set; }

    public Guid UserId { get; set; }

    public ICollection<CartItem>? CartItems { get; set; }

    public decimal TotalPrice => 
        CartItems?.Sum(item => item.SubTotalPrice) ?? 0;
}