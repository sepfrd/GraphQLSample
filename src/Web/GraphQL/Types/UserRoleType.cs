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
                    resolvers.GetUserAsync(default!, default!));

        descriptor
            .Field(userRole => userRole.Role)
            .ResolveWith<Resolvers>(resolvers => resolvers.GetRoleAsync(default!, default!));
    }

    private class Resolvers
    {
        public async Task<Role?> GetRoleAsync([Parent] UserRole userRole, [Service] ISender sender)
        {
            var rolesQuery = new GetAllRolesQuery(
                Pagination.MaxPagination,
                role => role.InternalId == userRole.RoleId);

            var result = await sender.Send(rolesQuery);

            return result.Data?.FirstOrDefault();
        }

        public async Task<User?> GetUserAsync([Parent] UserRole userRole, [Service] ISender sender)
        {
            var userQuery = new GetUserByInternalIdQuery(userRole.UserId);

            var result = await sender.Send(userQuery);

            return result.Data;
        }
    }
}