using Domain.Entities;
using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class CategoryFilterType : FilterInputType<Category>
{
    protected override void Configure(IFilterInputTypeDescriptor<Category> descriptor)
    {
        descriptor
            .BindFieldsExplicitly()
            .AllowAnd(false)
            .AllowOr(false)
            .Field(category => category.ExternalId)
            .Type<CustomIntOperationFilterType>();
    }
}