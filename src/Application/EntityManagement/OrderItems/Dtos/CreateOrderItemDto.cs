namespace Application.EntityManagement.OrderItems.Dtos;

public record CreateOrderItemDto
{
    public CreateOrderItemDto(int OrderExternalId,
        decimal UnitPrice,
        decimal SubTotalPrice,
        int Quantity)
    {
        this.OrderExternalId = OrderExternalId;
        this.UnitPrice = UnitPrice;
        this.SubTotalPrice = SubTotalPrice;
        this.Quantity = Quantity;
    }

    public int OrderExternalId { get; init; }

    public decimal UnitPrice { get; init; }

    public decimal SubTotalPrice { get; init; }

    public int Quantity { get; init; }

    public void Deconstruct(out int OrderExternalId, out decimal UnitPrice, out decimal SubTotalPrice, out int Quantity)
    {
        OrderExternalId = this.OrderExternalId;
        UnitPrice = this.UnitPrice;
        SubTotalPrice = this.SubTotalPrice;
        Quantity = this.Quantity;
    }
}