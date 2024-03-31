using Application.Common;
using Application.EntityManagement.Addresses.Dtos.AddressDto;
using Application.EntityManagement.Answers.Dtos.AnswerDto;
using Application.EntityManagement.Categories.Dtos.CategoryDto;
using Application.EntityManagement.Comments.Dtos.CommentDto;
using Application.EntityManagement.Orders.Dtos.CreateOrderDto;
using Application.EntityManagement.Payments.Dtos.PaymentDto;
using Application.EntityManagement.Persons.Dtos.PersonDto;
using Application.EntityManagement.PhoneNumbers.Dtos.PhoneNumberDto;
using Application.EntityManagement.Products.Dtos.ProductDto;
using Application.EntityManagement.Questions.Dtos.QuestionDto;
using Application.EntityManagement.Shipments.Dtos.ShipmentDto;
using Application.EntityManagement.Users.Dtos.CreateUserDto;
using Application.EntityManagement.Users.Dtos.LoginDto;
using Application.EntityManagement.Users.Dtos.UserDto;
using Application.EntityManagement.Votes.Dtos.VoteDto;
using Web.GraphQL.Types;
using Web.GraphQL.Types.SortTypes;

namespace Web;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddAllGraphQlServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddFairyBread()
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
            .AddType<AddressSortType>()
            .AddType<AnswerSortType>()
            .AddType<CartItemSortType>()
            .AddType<CategorySortType>()
            .AddType<CategorySortType>()
            .AddType<CommentSortType>()
            .AddType<OrderItemSortType>()
            .AddType<OrderSortType>()
            .AddType<PaymentSortType>()
            .AddType<PersonSortType>()
            .AddType<PhoneNumberSortType>()
            .AddType<ProductSortType>()
            .AddType<QuestionSortType>()
            .AddType<RoleSortType>()
            .AddType<ShipmentSortType>()
            .AddType<UserSortType>()
            .AddType<VoteSortType>()
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
            .AddSorting()
            .ModifyRequestOptions(options =>
            {
                options.Complexity.Enable = true;
                options.Complexity.MaximumAllowed = 300;
                options.ExecutionTimeout = TimeSpan.FromSeconds(1);
                options.IncludeExceptionDetails = false;
            });

        return services;
    }
}