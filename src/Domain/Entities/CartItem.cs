using Domain.Common;

namespace Domain.Entities;

public sealed class CartItem : BaseEntity
{
    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public decimal SubTotalPrice => UnitPrice * Quantity;

    public Product? Product { get; set; }

    public Cart? Cart { get; set; }
}