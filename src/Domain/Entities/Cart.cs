using Domain.Common;

namespace Domain.Entities;

public sealed class Cart : BaseEntity
{
    public ICollection<CartItem>? CartItems { get; set; }

    public User? User { get; set; }

    public decimal TotalPrice
    {
        get
        {
            if (CartItems is null || CartItems.Count == 0)
            {
                return 0;
            }

            return CartItems.Sum(item => item.SubTotalPrice);
        }
    }
}