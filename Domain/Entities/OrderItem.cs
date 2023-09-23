using Domain.Common;

namespace Domain.Entities;

public class OrderItem : BaseEntity
{
    public decimal UnitPrice { get; set; }
    public int Quantity { get; set; }
    public decimal TotalPrice => UnitPrice * Quantity;

    public Guid ProductId { get; set; }
    public Guid OrderId { get; set; }
}