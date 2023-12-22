using Application.Common;
using Application.EntityManagement.Addresses.Dtos;
using Application.EntityManagement.Answers.Dtos;
using Application.EntityManagement.Categories.Dtos;
using Application.EntityManagement.Comments.Dtos;
using Application.EntityManagement.Orders.Dtos;
using Application.EntityManagement.Payments.Dtos;
using Application.EntityManagement.Persons.Dtos;
using Application.EntityManagement.PhoneNumbers.Dtos;
using Application.EntityManagement.Products.Dtos;
using Application.EntityManagement.Questions.Dtos;
using Application.EntityManagement.Shipments.Dtos;
using Application.EntityManagement.Users.Dtos;
using Application.EntityManagement.Votes.Dtos;
using Web.GraphQL.Types;

namespace Web;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddAllGraphQlServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddAuthorization()
            .AddMaxExecutionDepthRule(15)
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
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
            .AddInputObjectType<CategoryDto>()
            .AddInputObjectType<CommentDto>()
            .AddInputObjectType<CreateOrderDto>()
            .AddInputObjectType<PaymentDto>()
            .AddInputObjectType<PersonDto>()
            .AddInputObjectType<PhoneNumberDto>()
            .AddInputObjectType<ProductDto>()
            .AddInputObjectType<QuestionDto>()
            .AddInputObjectType<ShipmentDto>()
            .AddInputObjectType<UserDto>()
            .AddInputObjectType<LoginDto>()
            .AddInputObjectType<CreateUserDto>()
            .AddInputObjectType<VoteDto>()
            .AddFiltering()
            .AddSorting();

        return services;
    }
}