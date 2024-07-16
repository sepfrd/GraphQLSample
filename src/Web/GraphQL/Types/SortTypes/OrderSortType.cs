using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class OrderSortType : SortInputType<Order>
{
    protected override void Configure(ISortInputTypeDescriptor<Order> descriptor)
    {
        descriptor
            .Field(order => order.InternalId)
            .Ignore();

        descriptor
            .Field(order => order.PaymentId)
            .Ignore();

        descriptor
            .Field(order => order.ShipmentId)
            .Ignore();
    }
}