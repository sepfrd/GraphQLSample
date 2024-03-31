using Domain.Entities;
using HotChocolate.Data.Sorting;

namespace Web.GraphQL.Types.SortTypes;

public class ProductSortType : SortInputType<Product>
{
    protected override void Configure(ISortInputTypeDescriptor<Product> descriptor)
    {
        descriptor
            .Field(product => product.InternalId)
            .Ignore();

        descriptor
            .Field(product => product.CategoryId)
            .Ignore();
    }
}