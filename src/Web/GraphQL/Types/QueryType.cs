using Application.Common.Constants;
using Web.GraphQL.Types.FilterTypes;

namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Authorize()
            .Description("Root Query That Provides Operations to Retrieve Data");

        descriptor
            .Field(_ =>
                Query.GetUsersAsync(null!, CancellationToken.None))
            .Authorize(PolicyConstants.AdminPolicy)
            .UsePaging()
            .Description("Retrieves a list of users.\n" +
                         "Requires admin privileges for access.\n" +
                         "Supports advanced querying with pagination.\n" +
                         "Authentication is mandatory for security purposes.");

        descriptor
            .Field(_ =>
                Query.GetCategoriesAsync(null!, CancellationToken.None))
            .AllowAnonymous()
            .UsePaging()
            .UseFiltering<CategoryFilterType>()
            .UseSorting()
            .Description("Retrieves the list of product categories.\n" +
                         "Allows anonymous access for public visibility.\n" +
                         "Supports filtering based on category criteria.\n" +
                         "Supports sorting for better data organization.");

        descriptor
            .Field(_ =>
                Query.GetProductsAsync(null!, CancellationToken.None))
            .AllowAnonymous()
            .UsePaging()
            .UseFiltering<ProductFilterType>()
            .UseSorting()
            .Description("Retrieves a list of products.\n" +
                         "Allows anonymous access for public visibility.\n" +
                         "Supports advanced querying with filtering options.\n" +
                         "Enables sorting for better control over the displayed data.");
    }
}