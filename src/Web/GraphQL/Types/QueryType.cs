using Application.Common;
using MediatR;

namespace Web.GraphQL.Types;

public class QueryType : ObjectType<Query>
{
    private readonly ISender _sender;
    private readonly Pagination _pagination = new();

    public QueryType(ISender sender)
    {
        _sender = sender;
    }

    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Field(query =>
                query.GetUsersAsync(_pagination, _sender, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(query =>
                query.GetCategoriesAsync(_pagination, _sender, default!))
            .UseFiltering()
            .UseSorting();

        descriptor
            .Field(query =>
                query.GetProductsAsync(_pagination, _sender, default!))
            .UseFiltering()
            .UseSorting();
    }
}