using Application.Common;
using Application.Common.Constants;
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
using Infrastructure;
using Infrastructure.Common.Configurations;
using Infrastructure.Services.Logging;
using Microsoft.Extensions.Options;
using Web.GraphQL.Types;
using Web.GraphQL.Types.SortTypes;

namespace Web;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDependencies(this IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
    {
        var appOptions = configuration.Get<AppOptions>()!;

        services
            .AddHttpContextAccessor()
            .AddEndpointsApiExplorer()
            .AddCors(options =>
            {
                options.AddPolicy(PolicyConstants.CorsPolicy, policyBuilder =>
                {
                    policyBuilder
                        .AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod();
                });
            })
            .AddSingleton(Options.Create(appOptions))
            .AddSingleton<ILogger, CustomLogger>()
            .AddInfrastructure(appOptions)
            .AddAllGraphQLServices(appOptions)
            .AddHealthChecks();

        return services;
    }

    private static IServiceCollection AddAllGraphQLServices(this IServiceCollection services, AppOptions appOptions)
    {
        services
            .AddGraphQLServer()
            .AddFairyBread()
            .AddAuthorization()
            .AddMaxExecutionDepthRule(15)
            .AddQueryType<QueryType>()
            .AddMutationType<MutationType>()
            .AddSubscriptionType<SubscriptionType>()
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
            .ModifyPagingOptions(options =>
            {
                options.IncludeTotalCount = appOptions.GraphQLOptions!.IncludeTotalCount;
                options.MaxPageSize = appOptions.GraphQLOptions.MaxPageSize;
            })
            .AddFiltering()
            .AddSorting()
            .ModifyRequestOptions(options =>
            {
                options.ExecutionTimeout = TimeSpan.FromSeconds(appOptions.GraphQLOptions!.ExecutionTimeoutSeconds);
                options.IncludeExceptionDetails = appOptions.GraphQLOptions.IncludeExceptionDetails;
            })
            .ModifyCostOptions(options =>
            {
                options.MaxFieldCost = appOptions.GraphQLOptions!.MaxFieldCost;
                options.MaxTypeCost = appOptions.GraphQLOptions.MaxTypeCost;
                options.EnforceCostLimits = appOptions.GraphQLOptions.EnforceCostLimits;
                options.ApplyCostDefaults = appOptions.GraphQLOptions.ApplyCostDefaults;
                options.DefaultResolverCost = appOptions.GraphQLOptions.DefaultResolverCost;
            })
            .AddInMemorySubscriptions();

        return services;
    }
}