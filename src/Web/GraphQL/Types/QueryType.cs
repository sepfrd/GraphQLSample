using Web.GraphQL.Types.FilterTypes;

namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Description("Root Query That Provides Operations to Retrieve Data");

        descriptor
            .Authorize()
            .Field(_ =>
                Query.GetEmployeesAsync(null!, CancellationToken.None))
            .UsePaging()
            .UseFiltering<EmployeeFilterType>()
            .UseSorting()
            .Description("Retrieves a list of employees.\n" +
                         "Supports advanced querying with pagination.\n" +
                         "Authentication is mandatory for security purposes.");

        descriptor
            .Authorize()
            .Field(_ =>
                Query.GetProjectsAsync(null!, CancellationToken.None))
            .UsePaging()
            .UseFiltering<ProjectFilterType>()
            .UseSorting()
            .Description("Retrieves a list of projects.\n" +
                         "Supports advanced querying with pagination.\n" +
                         "Authentication is mandatory for security purposes.");

        descriptor
            .Authorize()
            .Field(_ =>
                Query.GetDepartmentsAsync(null!, CancellationToken.None))
            .UsePaging()
            .UseFiltering<DepartmentFilterType>()
            .UseSorting()
            .Description("Retrieves a list of departments.\n" +
                         "Supports advanced querying with pagination.\n" +
                         "Authentication is mandatory for security purposes.");
    }
}