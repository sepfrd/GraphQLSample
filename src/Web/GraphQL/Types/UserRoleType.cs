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
            .Field(userRole => userRole.User)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetUserAsync(default!, default!));

        descriptor
            .Field(userRole => userRole.Role)
            .ResolveWith<Resolvers>(
                resolvers =>
                    Resolvers.GetRoleAsync(default!, default!));
    }

    private class Resolvers
    {
        public static async Task<Role?> GetRoleAsync([Parent] UserRole userRole, [Service] ISender sender)
        {
            var rolesQuery = new GetAllRolesQuery(
                new Pagination(1, int.MaxValue),
                role => role.InternalId == userRole.RoleId
                );

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