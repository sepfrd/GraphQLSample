using HotChocolate.Data.Filters;

namespace Web.GraphQL.Types.FilterTypes;

public class CustomStringOperationFilterType : StringOperationFilterInputType
{
    protected override void Configure(IFilterInputTypeDescriptor descriptor)
    {
        descriptor
            .AllowAnd(false)
            .AllowOr(false)
            .Operation(DefaultFilterOperations.In)
            .Operation(DefaultFilterOperations.NotIn)
            .Operation(DefaultFilterOperations.GreaterThan)
            .Operation(DefaultFilterOperations.NotGreaterThan)
            .Operation(DefaultFilterOperations.GreaterThanOrEquals)
            .Operation(DefaultFilterOperations.NotGreaterThanOrEquals)
            .Operation(DefaultFilterOperations.LowerThan)
            .Operation(DefaultFilterOperations.NotLowerThan)
            .Operation(DefaultFilterOperations.LowerThanOrEquals)
            .Operation(DefaultFilterOperations.NotLowerThanOrEquals)
            .Operation(DefaultFilterOperations.NotEquals)
            .Ignore();

        descriptor
            .Operation(DefaultFilterOperations.Equals)
            .Type<StringType>();

        descriptor
            .Operation(DefaultFilterOperations.Contains)
            .Type<StringType>();

        descriptor
            .Operation(DefaultFilterOperations.StartsWith)
            .Type<StringType>();

        descriptor
            .Operation(DefaultFilterOperations.EndsWith)
            .Type<StringType>();

        descriptor
            .Operation(DefaultFilterOperations.NotContains)
            .Type<StringType>();
    }
}