using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PersonType : ObjectType<Person>
{
    protected override void Configure(IObjectTypeDescriptor<Person> descriptor)
    {
        descriptor
            .Description("Represents an individual person with details such as first name, last name, and birth date.");

        descriptor
            .Field(person => person.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(null!, null!))
            .Description("The User Associated with the Person\n" +
                         "Authentication is required.");

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
        public async static Task<User?> GetUserAsync([Parent] Person person, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(x => x.InternalId == person.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}