using Application.Common.Constants;

namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Authorize();

        descriptor
            .Field(query =>
                Query.GetUsersAsync(default, default, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(query =>
                Query.GetCategoriesAsync(default, default!, default!))
            .AllowAnonymous()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(query =>
                Query.GetProductsAsync(default, default, default!, default!))
            .AllowAnonymous()
            .UseFiltering()
            .UseSorting();
    }
}