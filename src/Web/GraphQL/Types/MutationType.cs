using Application.Common.Constants;

namespace Web.GraphQL.Types;

public sealed class MutationType : ObjectType<Mutation>
{
    protected override void Configure(IObjectTypeDescriptor<Mutation> descriptor)
    {
        descriptor
            .Authorize()
            .Field(mutation => Mutation.LoginAsync(default!, default!, default!))
            .AllowAnonymous();

        descriptor
            .Field(mutation =>
                Mutation.SignUpAsync(default!, default!, default!))
            .AllowAnonymous();

        descriptor
            .Field(mutation =>
                Mutation.AddAddressAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateAddressAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteAddressAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddAnswerAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateAnswerAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteAnswerAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddCartItemAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteCartItemAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddCategoryAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddCommentAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteCommentAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddOrderAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateOrderAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteOrderAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdatePaymentAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdatePersonAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddPhoneNumberAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdatePhoneNumberAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeletePhoneNumberAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddProductAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateProductAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteProductAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddQuestionAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteQuestionAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddRoleAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateRoleAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteRoleAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateShipmentAsync(default!, default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddUserRoleAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteUserRoleAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.UpdateUserAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteUserAsync(default!, default!, default!))
            .Authorize(PolicyConstants.AdminPolicy);

        descriptor
            .Field(mutation =>
                Mutation.AddVoteAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);

        descriptor
            .Field(mutation =>
                Mutation.DeleteVoteAsync(default!, default!, default!))
            .Authorize(PolicyConstants.CustomerPolicy);
    }
}