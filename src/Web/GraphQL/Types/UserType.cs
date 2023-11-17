using Application.Common;
using Application.EntityManagement.Carts.Queries;
using Application.EntityManagement.Persons.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class UserType : ObjectType<User>
{
    protected override void Configure(IObjectTypeDescriptor<User> descriptor)
    {
        descriptor
            .Field(user => user.Cart)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetCartAsync(default!, default!));

        descriptor
            .Field(user => user.Person)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetPersonAsync(default!, default!));
        descriptor
            .Field(user => user.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(user => user.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(user => user.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(user => user.InternalId)
            .Ignore();

        descriptor
            .Field(user => user.CartId)
            .Ignore();

        descriptor
            .Field(user => user.PersonId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<Person?> GetPersonAsync([Parent] User user, [Service] ISender sender)
        {
            var personsQuery = new GetAllPersonsQuery(
                new Pagination(),
                null,
                x => x.InternalId == user.PersonId);

            var result = await sender.Send(personsQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<Cart?> GetCartAsync([Parent] User user, [Service] ISender sender)
        {
            var cartsQuery = new GetAllCartsQuery(
                new Pagination(),
                null,
                x => x.InternalId == user.CartId);

            var result = await sender.Send(cartsQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}