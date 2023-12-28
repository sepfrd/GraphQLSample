using Application.EntityManagement.CartItems.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class CartType : ObjectType<Cart>
{
    protected override void Configure(IObjectTypeDescriptor<Cart> descriptor)
    {
        descriptor
            .Description("Represents a user's shopping cart, containing a list of cart items and associated user information.");

        descriptor
            .Field(cart => cart.User)
            .Description("The User This Cart Belongs To")
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(default!, default!));

        descriptor
            .Field(cart => cart.CartItems)
            .Description("All Sub Items of This Cart")
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetCartItemsAsync(default!, default!));

        descriptor
            .Field(cart => cart.TotalPrice)
            .Description("Total Price of This Cart");

        descriptor
            .Field(cart => cart.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(cart => cart.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(cart => cart.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(cart => cart.InternalId)
            .Ignore();

        descriptor
            .Field(cart => cart.UserId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public static async Task<User?> GetUserAsync([Parent] Cart cart, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(
                new Pagination(),
                x => x.InternalId == cart.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<IEnumerable<CartItem>?> GetCartItemsAsync([Parent] Cart cart, [Service] ISender sender)
        {
            var cartItemsQuery = new GetAllCartItemsQuery(
                new Pagination(),
                x => x.CartId == cart.InternalId);

            var result = await sender.Send(cartItemsQuery);

            return result.Data;
        }
    }
}