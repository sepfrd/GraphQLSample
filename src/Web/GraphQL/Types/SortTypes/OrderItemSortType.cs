using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class OrderItemSortType : SortInputType<OrderItem>
{
    protected override void Configure(ISortInputTypeDescriptor<OrderItem> descriptor)
    {
        descriptor
            .Field(orderItem => orderItem.InternalId)
            .Ignore();

        descriptor
            .Field(orderItem => orderItem.OrderId)
            .Ignore();

        descriptor
            .Field(orderItem => orderItem.ProductId)
            .Ignore();
    }
}