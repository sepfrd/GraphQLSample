namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor.Authorize();

        descriptor
            .Field(query =>
                query.GetUsersAsync(default, default, default!, default!));

        descriptor
            .Field(query =>
                query.GetCategoriesAsync(default, default!, default!))
            .AllowAnonymous()
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(query =>
                query.GetProductsAsync(default, default, default!, default!))
            .AllowAnonymous()
            .UseFiltering()
            .UseSorting();
    }
}