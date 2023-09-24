using Domain.Common;

namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal SubTotalPrice => UnitPrice * Quantity;

    public Product? Product { get; set; }
    public Order? Order { get; set; }
}