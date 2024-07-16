using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class CartItemSortType : SortInputType<CartItem>
{
    protected override void Configure(ISortInputTypeDescriptor<CartItem> descriptor)
    {
        descriptor
            .Field(cartItem => cartItem.InternalId)
            .Ignore();

        descriptor
            .Field(cartItem => cartItem.CartId)
            .Ignore();

        descriptor
            .Field(cartItem => cartItem.ProductId)
            .Ignore();

        descriptor
            .Field(cartItem => cartItem.Cart)
            .Ignore();
    }
}