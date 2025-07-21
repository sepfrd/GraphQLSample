namespace Web.GraphQL.Types;

public sealed class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Authorize()
            .Description("Root Mutation That Provides Operations to Modify Data");

        descriptor
            .Field(_ => Mutation.LoginAsync(null!, null!, CancellationToken.None))
            .AllowAnonymous()
            .Description("Allows users to log in and obtain authentication credentials.\n" +
                         "Requires valid username/email and password.\n" +
                         "Returns an authentication token upon successful login.");
    }
}