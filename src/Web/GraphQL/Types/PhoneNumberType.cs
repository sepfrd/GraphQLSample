using Application.EntityManagement.Users.Queries;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PhoneNumberType : ObjectType<PhoneNumber>
{
    protected override void Configure(IObjectTypeDescriptor<PhoneNumber> descriptor)
    {
        descriptor
            .Description(
                "Represents a phone number associated with a user, including details like the number itself and its type.");

        descriptor
            .Field(phoneNumber => phoneNumber.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(null!, null!))
            .Description("The User Associated with the Phone Number\n" +
                         "Authentication is required.");

        descriptor
            .Field(phoneNumber => phoneNumber.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(phoneNumber => phoneNumber.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(phoneNumber => phoneNumber.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(phoneNumber => phoneNumber.InternalId)
            .Ignore();

        descriptor
            .Field(phoneNumber => phoneNumber.UserId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public async static Task<User?> GetUserAsync([Parent] PhoneNumber phoneNumber, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(x => x.InternalId == phoneNumber.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}