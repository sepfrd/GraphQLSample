using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.CartItems.Dtos;
using Application.EntityManagement.Carts.Dtos;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.OrderItems.Dtos;
using Application.EntityManagement.Orders.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Persons.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.Products.Dtos;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Users.Dtos;
using Web.GraphQL.Types;
using Web.GraphQL.Types.InputTypes;

namespace Web;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddAllGraphQlServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddMaxExecutionDepthRule(10)
            .AddType<AddressType>()
            .AddType<AnswerType>()
            .AddType<CartItemType>()
            .AddType<CartType>()
            .AddType<CategoryType>()
            .AddType<CategoryType>()
            .AddType<CommentType>()
            .AddType<OrderItemType>()
            .AddType<OrderType>()
            .AddType<PaymentType>()
            .AddType<PersonType>()
            .AddType<PhoneNumberType>()
            .AddType<ProductType>()
            .AddType<QuestionType>()
            .AddType<RoleType>()
            .AddType<ShipmentType>()
            .AddType<UserType>()
            .AddType<VoteType>()
            .AddType<CommandResult>()
            .AddType<UserRoleType>()
            .AddInputObjectType<AddressDto>()
            .AddInputObjectType<AnswerDto>()
            .AddInputObjectType<CartItemDto>()
            .AddInputObjectType<CartDto>()
            .AddInputObjectType<CategoryDto>()
            .AddInputObjectType<CommentDto>()
            .AddInputObjectType<OrderItemDto>()
            .AddInputObjectType<OrderDto>()
            .AddInputObjectType<PaymentDto>()
            .AddInputObjectType<PersonDto>()
            .AddInputObjectType<PhoneNumberDto>()
            .AddInputObjectType<ProductDto>()
            .AddInputObjectType<QuestionDto>()
            .AddInputObjectType<ShipmentDto>()
            .AddInputObjectType<UserDto>()
            .AddInputObjectType<CreateUserDto>()
            .AddInputObjectType<AddAnswerVoteInputType>()
            .AddInputObjectType<AddCommentVoteInputType>()
            .AddInputObjectType<AddProductVoteInputType>()
            .AddInputObjectType<AddQuestionVoteInputType>()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}