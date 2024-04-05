using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class UserSortType : SortInputType<User>
{
    protected override void Configure(ISortInputTypeDescriptor<User> descriptor)
    {
        descriptor
            .Field(user => user.InternalId)
            .Ignore();

        descriptor
            .Field(user => user.Password)
            .Ignore();

        descriptor
            .Field(user => user.CartId)
            .Ignore();

        descriptor
            .Field(user => user.PersonId)
            .Ignore();

        descriptor
            .Field(user => user.Cart)
            .Ignore();
    }
}