using Application.EntityManagement.Carts.Queries;
using Application.EntityManagement.Products.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class CartItemType : ObjectType<CartItem>
{
    protected override void Configure(IObjectTypeDescriptor<CartItem> descriptor)
    {
        descriptor
            .Description(
                "Represents an item added to a user's shopping cart, including details like product, quantity, unit price, and subtotal.");

        descriptor
            .Field(cartItem => cartItem.Quantity)
            .Description("The Number of the Single Product");

        descriptor
            .Field(cartItem => cartItem.UnitPrice)
            .Description("The Price of a Single Product");

        descriptor
            .Field(cartItem => cartItem.SubTotalPrice)
            .Description("The Total Price of This Cart Item.");

        descriptor
            .Field(cartItem => cartItem.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(cartItem => cartItem.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(cartItem => cartItem.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(cartItem => cartItem.Cart)
            .Description("The Cart This Item Belongs To")
            .ResolveWith<Resolvers>(resolvers =>
                Resolvers.GetCartAsync(null!, null!));

        descriptor
            .Field(cartItem => cartItem.Product)
            .Description("The Product This Cart Item Holds")
            .ResolveWith<Resolvers>(resolvers =>
                Resolvers.GetProductAsync(null!, null!));

        descriptor
            .Field(cartItem => cartItem.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(cartItem => cartItem.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(cartItem => cartItem.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(cartItem => cartItem.InternalId)
            .Ignore();

        descriptor
            .Field(cartItem => cartItem.CartId)
            .Ignore();

        descriptor
            .Field(cartItem => cartItem.ProductId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async static Task<Cart?> GetCartAsync([Parent] CartItem cartItem, [Service] ISender sender)
        {
            var cartsQuery = new GetAllCartsQuery(x => x.InternalId == cartItem.CartId);

            var result = await sender.Send(cartsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async static Task<Product?> GetProductAsync([Parent] CartItem cartItem, [Service] ISender sender)
        {
            var productsQuery = new GetAllProductsQuery(x => x.InternalId == cartItem.ProductId);

            var result = await sender.Send(productsQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}