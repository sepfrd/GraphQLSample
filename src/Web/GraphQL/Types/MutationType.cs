using Application.Common.Constants;

namespace Web.GraphQL.Types;

public sealed class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Authorize()
            .Field(mutation => mutation.LoginAsync(default!, default!, default!))
            .AllowAnonymous();

        descriptor
            .Field(mutation =>
                mutation.AddAddressAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdateAddressAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteAddressAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddAnswerAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdateAnswerAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteAnswerAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddCartItemAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteCartItemAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddCategoryAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddCommentAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteCommentAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddOrderAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdateOrderAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteOrderAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdatePaymentAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdatePersonAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddPhoneNumberAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdatePhoneNumberAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeletePhoneNumberAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddProductAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdateProductAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteProductAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddQuestionAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteQuestionAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdateShipmentAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddUserAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.UpdateUserAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteUserAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                mutation.AddVoteAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                mutation.DeleteVoteAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);
    }
}