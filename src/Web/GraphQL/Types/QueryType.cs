namespace Web.GraphQL.Types;

public sealed class QueryType : ObjectType<Query>
{
    protected override void Configure(IObjectTypeDescriptor<Query> descriptor)
    {
        descriptor
            .Authorize()
            .Description("Root Query That Provides Operations to Retrieve Data");
    }
}