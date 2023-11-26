using Application.Abstractions;
using Application.EntityManagement.Users.Dtos;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services.Mapping;
using Mapster;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;

namespace Infrastructure;

public static class DependencyInjectionHelper
{
    public static IServiceCollection InjectInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureMapster();

        return services
            .AddMongoDb(configuration)
            .AddRepositories()
            .AddSingleton<IMappingService, MappingService>();
    }

    private static IServiceCollection AddRepositories(this IServiceCollection services) =>
        services
            .AddScoped<IRepository<Address>, AddressRepository>()
            .AddScoped<IRepository<Answer>, AnswerRepository>()
            .AddScoped<IRepository<CartItem>, CartItemRepository>()
            .AddScoped<IRepository<Cart>, CartRepository>()
            .AddScoped<IRepository<Category>, CategoryRepository>()
            .AddScoped<IRepository<Comment>, CommentRepository>()
            .AddScoped<IRepository<OrderItem>, OrderItemRepository>()
            .AddScoped<IRepository<Order>, OrderRepository>()
            .AddScoped<IRepository<Payment>, PaymentRepository>()
            .AddScoped<IRepository<Person>, PersonRepository>()
            .AddScoped<IRepository<PhoneNumber>, PhoneNumberRepository>()
            .AddScoped<IRepository<Product>, ProductRepository>()
            .AddScoped<IRepository<Question>, QuestionRepository>()
            .AddScoped<IRepository<Role>, RoleRepository>()
            .AddScoped<IRepository<Shipment>, ShipmentRepository>()
            .AddScoped<IRepository<User>, UserRepository>()
            .AddScoped<IRepository<UserRole>, UserRoleRepository>()
            .AddScoped<IRepository<Vote>, VoteRepository>();

    private static IServiceCollection AddMongoDb(this IServiceCollection services, IConfiguration configuration)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        BsonClassMap.RegisterClassMap<BaseEntity>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapIdMember(baseEntity => baseEntity.InternalId);
        });

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

        return services;
    }

    private static void ConfigureMapster()
    {
        TypeAdapterConfig<User, UserDto>
            .NewConfig()
            .Map(
                userDto => userDto.QuestionsCount,
                user => user.Questions == null ? 0 : user.Questions.Count)
            .Map(
                userDto => userDto.OrdersCount,
                user => user.Orders == null ? 0 : user.Orders.Count)
            .Map(
                userDto => userDto.VotesCount,
                user => user.Votes == null ? 0 : user.Votes.Count)
            .Map(
                userDto => userDto.AnswersCount,
                user => user.Answers == null ? 0 : user.Answers.Count);
    }
}