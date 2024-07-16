using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class AddressSortType : SortInputType<Address>
{
    protected override void Configure(ISortInputTypeDescriptor<Address> descriptor)
    {
        descriptor
            .Field(address => address.InternalId)
            .Ignore();

        descriptor
            .Field(address => address.UserId)
            .Ignore();
    }
}