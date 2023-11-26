using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PersonType : ObjectType<Person>
{
    protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
    {
        descriptor
            .Field(person => person.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetUserAsync(default!, default!));

        descriptor
            .Field(person => person.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(person => person.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(person => person.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(person => person.InternalId)
            .Ignore();

        descriptor
            .Field(person => person.UserId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async Task<User?> GetUserAsync([Parent] Person person, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(
                new Pagination(),
                x => x.InternalId == person.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}