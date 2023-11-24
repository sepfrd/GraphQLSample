using Application.Common;
using Application.EntityManagement.UserRoles.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class RoleType : ObjectType<Role>
{
    protected override void Configure(IObjectTypeDescriptor<Role> descriptor)
    {
        descriptor
            .Field(role => role.DateCreated)
            .Description("The Creation Date");

        descriptor
            .Field(role => role.DateModified)
            .Description("The Last Modification Date");

        descriptor
            .Field(role => role.ExternalId)
            .Description("The External ID for Client Interactions");

        descriptor
            .Field(role => role.UserRoles)
            .ResolveWith<Resolvers>(resolvers => Resolvers.GetUserRolesAsync(default!, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(role => role.InternalId)
            .Ignore();
    }

    private class Resolvers
    {
        public static async Task<IEnumerable<UserRole>?> GetUserRolesAsync([Parent] Role role, [Service] ISender sender)
        {
            var userRolesQuery = new GetAllUserRolesQuery(
                new Pagination(1, int.MaxValue),
                null,
                userRole => userRole.RoleId == role.InternalId);

            var result = await sender.Send(userRolesQuery);

            return result.Data;
        }
    }
}