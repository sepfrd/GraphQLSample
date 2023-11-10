using Domain.Entities;
using HotChocolate.Types;

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
            .Field(role => role.InternalId)
            .Ignore();
    }
}