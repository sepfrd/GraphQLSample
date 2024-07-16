using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class CategorySortType : SortInputType<Category>
{
    protected override void Configure(ISortInputTypeDescriptor<Category> descriptor)
    {
        descriptor
            .Field(category => category.InternalId)
            .Ignore();
    }
}