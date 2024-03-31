using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class PhoneNumberSortType : SortInputType<PhoneNumber>
{
    protected override void Configure(ISortInputTypeDescriptor<PhoneNumber> descriptor)
    {
        descriptor
            .Field(phoneNumber => phoneNumber.InternalId)
            .Ignore();

        descriptor
            .Field(phoneNumber => phoneNumber.UserId)
            .Ignore();
    }
}