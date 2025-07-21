using Infrastructure.Common.Constants;
using Web.GraphQL.Types.FilterTypes;

namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Description("Root Query That Provides Operations to Retrieve Data");

        descriptor
            .Authorize(PolicyConstants.AdminPolicy)
            .Field(_ =>
                Query.GetEmployeesAsync(null!, CancellationToken.None))
            .UsePaging()
            .UseFiltering<EmployeeFilterType>()
            .UseSorting()
            .Description("Retrieves a list of employees.\n" +
                         "Requires admin privileges for access.\n" +
                         "Supports advanced querying with pagination.\n" +
                         "Authentication is mandatory for security purposes.");
    }
}