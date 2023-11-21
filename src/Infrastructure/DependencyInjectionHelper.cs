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
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.IdGenerators;
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
        BsonClassMap.RegisterClassMap<BaseEntity>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapIdMember(baseEntity => baseEntity.InternalId)
                .SetIdGenerator(GuidGenerator.Instance)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Address>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(address => address.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Answer>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(answer => answer.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(answer => answer.QuestionId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Cart>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(cart => cart.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<CartItem>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(cartItem => cartItem.CartId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(cartItem => cartItem.ProductId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Comment>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(comment => comment.ProductId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(comment => comment.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Order>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(order => order.PaymentId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(order => order.ShipmentId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(order => order.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<OrderItem>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(orderItem => orderItem.OrderId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(orderItem => orderItem.ProductId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Payment>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(payment => payment.OrderId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(payment => payment.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Person>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(person => person.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<PhoneNumber>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(phoneNumber => phoneNumber.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Product>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(product => product.CategoryId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Question>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(question => question.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Shipment>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(shipment => shipment.OrderId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(shipment => shipment.DestinationAddressId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(shipment => shipment.OriginAddressId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(shipment => shipment.TraceId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<User>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(user => user.CartId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(user => user.PersonId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        BsonClassMap.RegisterClassMap<Vote>(classMap =>
        {
            classMap.AutoMap();
            classMap
                .MapMember(vote => vote.UserId)
                .SetSerializer(GuidSerializer.StandardInstance);
            classMap
                .MapMember(vote => vote.ContentId)
                .SetSerializer(GuidSerializer.StandardInstance);
        });

        services.Configure<MongoDbSettings>(mongoDbSettings =>
        {
            mongoDbSettings.ConnectionString = configuration
                .GetSection("MongoDb")
                .GetSection("ConnectionString")
                .Value!;

            mongoDbSettings.DatabaseName = configuration
                .GetSection("MongoDb")
                .GetSection("DatabaseName")
                .Value!;
        });

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