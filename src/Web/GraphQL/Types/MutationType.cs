namespace Web.GraphQL.Types;

public sealed class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Authorize()
            .Field(
                mutation => mutation.LoginAsync(default!, default!, default!))
            .AllowAnonymous();
    }
}