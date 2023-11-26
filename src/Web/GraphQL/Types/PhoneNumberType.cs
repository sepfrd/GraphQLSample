using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class PhoneNumberType : ObjectType<PhoneNumber>
{
    protected override void Configure(IObjectTypeDescriptor<PhoneNumber> descriptor)
    {
        descriptor
            .Field(phoneNumber => phoneNumber.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    resolvers.GetUserAsync(default!, default!));

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
        public async Task<User?> GetUserAsync([Parent] PhoneNumber phoneNumber, [Service] ISender sender)
        {
            var usersQuery = new GetAllUsersQuery(
                new Pagination(),
                x => x.InternalId == phoneNumber.UserId);

            var result = await sender.Send(usersQuery);

            return result.Data?.FirstOrDefault();
        }
    }
}