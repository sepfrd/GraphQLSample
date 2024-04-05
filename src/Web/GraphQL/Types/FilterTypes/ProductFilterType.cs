using Domain.Entities;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class ProductFilterType : FilterInputType<Product>
{
    protected override void Configure(IFilterInputTypeDescriptor<Product> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .AllowAnd(false)
            .AllowOr(false);

        descriptor
            .Field(product => product.ExternalId)
            .Type<CustomIntOperationFilterType>();

        descriptor
            .Field(product => product.Name)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(product => product.Description)
            .Type<CustomStringOperationFilterType>();

        descriptor
            .Field(product => product.Price)
            .Type<CustomDecimalOperationFilterType>();
    }
}