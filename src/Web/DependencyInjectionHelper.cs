using Infrastructure.Services.Logging;
using Serilog;
using Web.GraphQL;
using Web.GraphQL.Types;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace Web;

public static class DependencyInjectionHelper
{
    public static IServiceCollection AddAllGraphQlServices(this IServiceCollection services)
    {
        services
            .AddGraphQLServer()
            .AddQueryType<Query>()
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
            .AddType<VoteType>();

        return services;
    }

    public static IServiceCollection AddLogger(this IServiceCollection services, IConfiguration configuration)
    {
        var logger = new LoggerConfiguration()
            .ReadFrom
            .Configuration(configuration)
            .CreateLogger();

        var customLogger = new CustomLogger(logger);

        return services.AddSingleton<ILogger>(customLogger);
    }
}