using Application.EntityManagement.Roles.Queries;
using Application.EntityManagement.Users.Queries;
using Domain.Common;
using Domain.Entities;
using MediatR;

namespace Web.GraphQL.Types;

public class UserRoleType : ObjectType<UserRole>
{
    protected override void Configure(IObjectTypeDescriptor<UserRole> descriptor)
    {
        descriptor
            .Description("Represents the association between a user and a role, indicating the roles assigned to a user.");

        descriptor
            .Field(userRole => userRole.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(default!, default!))
            .Description("The User Associated with the User-Role\n" +
                         "Authentication is required.");

        descriptor
            .Field(userRole => userRole.Role)
            .ResolveWith<Resolvers>(resolvers => Resolvers.GetRoleAsync(default!, default!))
            .Description("The Role Associated with the User-Role\n" +
                         "Authentication is required.");
        
        descriptor
            .Field(vote => vote.InternalId)
            .Ignore();

        descriptor
            .Field(vote => vote.UserId)
            .Ignore();

        descriptor
            .Field(vote => vote.RoleId)
            .Ignore();
    }

    private class Resolvers
    {
        public static async Task<Role?> GetRoleAsync([Parent] UserRole userRole, [Service] ISender sender)
        {
            var rolesQuery = new GetAllRolesQuery(
                Pagination.MaxPagination,
                role => role.InternalId == userRole.RoleId);

            var result = await sender.Send(rolesQuery);

            return result.Data?.FirstOrDefault();
        }

        public static async Task<User?> GetUserAsync([Parent] UserRole userRole, [Service] ISender sender)
        {
            var userQuery = new GetUserByInternalIdQuery(userRole.UserId);

            var result = await sender.Send(userQuery);

            return result.Data;
        }
    }
}