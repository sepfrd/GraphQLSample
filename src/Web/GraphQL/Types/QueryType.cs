namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{ 
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(query =>
                query.GetUsersAsync(default, default, default!, default!))
            .Authorize()
            .UseFiltering()
            .UseSorting();

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