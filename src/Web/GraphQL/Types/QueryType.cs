using Application.Common.Constants;

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
                Query.GetUsersAsync(default, default, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy)
            .Description("Retrieves a list of users.\n" +
                         "Requires admin privileges for access.\n" +
                         "Supports advanced querying with pagination.\n" +
                         "Authentication is mandatory for security purposes.");

        descriptor
            .Field(_ =>
                Query.GetCategoriesAsync(default, default!, default!))
            .AllowAnonymous()
            .UseSorting()
            .Description("Retrieves the list of product categories.\n" +
                         "Allows anonymous access for public visibility.\n" +
                         "Supports filtering based on category criteria.\n" +
                         "Supports sorting for better data organization.");

        descriptor
            .Field(_ =>
                Query.GetProductsAsync(default, default, default!, default!))
            .AllowAnonymous()
            .UseSorting()
            .Description("Retrieves a list of products.\n" +
                         "Allows anonymous access for public visibility.\n" +
                         "Supports advanced querying with filtering options.\n" +
                         "Enables sorting for better control over the displayed data.");
    }
}