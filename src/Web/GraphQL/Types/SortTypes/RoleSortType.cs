using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class RoleSortType : SortInputType<Role>
{
    protected override void Configure(ISortInputTypeDescriptor<Role> descriptor)
    {
        descriptor
            .Field(role => role.InternalId)
            .Ignore();
    }
}