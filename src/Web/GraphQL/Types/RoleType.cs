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
            .Description("Represents a user role, including details like title and description.");

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
            .UseSorting()
            .Description("The User-Roles Associated with the Role\n" +
                         "Authentication is required.");

        descriptor
            .Field(role => role.InternalId)
            .Ignore();
    }

    private sealed class Resolvers
    {
        public static async Task<IEnumerable<UserRole>?> GetUserRolesAsync([Parent] Role role, [Service] ISender sender)
        {
            var userRolesQuery = new GetAllUserRolesQuery(
                Pagination.MaxPagination,
                userRole => userRole.RoleId == role.InternalId);

            var result = await sender.Send(userRolesQuery);

            return result.Data;
        }
    }
}