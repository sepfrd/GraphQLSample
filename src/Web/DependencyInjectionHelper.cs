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
using Web.GraphQL;
using Web.GraphQL.Types;

namespace Web;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddAllGraphQlServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
            .AddMutationType<Mutation>()
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
            .AddInputObjectType<CreateUserDto>();

        return services;
    }
}