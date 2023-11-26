#region

using MediatR;

#endregion

namespace Web.GraphQL.Types;

public class QueryType : ObjectType<Query>
{
    private readonly ISender _sender;

    public QueryType(ISender sender)
    {
        _sender = sender;
    }

    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(query =>
                query.GetUsersAsync(default, default, _sender, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(query =>
                query.GetCategoriesAsync(default, _sender, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(query =>
                query.GetProductsAsync(default, default, _sender, default!))
            .UseFiltering()
            .UseSorting();
    }
}