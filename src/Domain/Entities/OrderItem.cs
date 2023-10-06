using Domain.Common;

namespace Domain.Entities;

public sealed class OrderItem : BaseEntity
{
    public decimal UnitPrice { get; set; }

    public int Quantity { get; set; }

    public decimal SubTotalPrice => UnitPrice * Quantity;

    public Guid ProductId { get; set; }

    public Guid OrderId { get; set; }
}