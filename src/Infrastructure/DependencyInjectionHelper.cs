using Application.Abstractions;
using Application.Common.Constants;
using Application.EntityManagement.Users.Dtos;
using Domain.Abstractions;
using Domain.Common;
using Domain.Entities;
using Humanizer;
using Infrastructure.Persistence.Common;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services;
using Infrastructure.Services.Mapping;
using Mapster;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;
using System.Security.Cryptography;

namespace Infrastructure;

public static class DependencyInjectionHelper
{
    public static IServiceCollection InjectInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        ConfigureMapster();

        return services
            .AddMongoDb(configuration)
            .AddRepositories()
            .AddSingleton<IMappingService, MappingService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddAuth(configuration);
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

        var connectionString = configuration.GetSection("MongoDb").GetValue<string>("ConnectionString");
        var databaseName = configuration.GetSection("MongoDb").GetValue<string>("DatabaseName");

        var mongoClient = new MongoClient(connectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseName);

        CreateIndexes(mongoDatabase);

        services.Configure<MongoDbSettings>(configuration.GetSection("MongoDb"));

        return services;
    }

    private static IServiceCollection AddAuth(this IServiceCollection services, IConfiguration configuration) =>
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var publicKey = configuration.GetSection("JwtConfiguration").GetValue<string>("PublicKey");

                var rsa = RSA.Create();

                rsa.ImportFromPem(publicKey);

                var securityKey = new RsaSecurityKey(rsa);

                options.SaveToken = true;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = securityKey,
                    ValidIssuer = DomainConstants.ApplicationUrl,
                    ValidAudience = DomainConstants.ApplicationUrl
                };
            })
            .Services
            .AddAuthorization(options =>
            {
                options.AddPolicy(
                    PolicyConstants.AddToCart,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Customer));

                options.AddPolicy(
                    PolicyConstants.PlaceOrder,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Customer));

                options.AddPolicy(
                    PolicyConstants.ManageProducts,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Admin));

                options.AddPolicy(
                    PolicyConstants.ManageOrders,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Admin));

                options.AddPolicy(
                    PolicyConstants.ManageUsers,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Admin));

                options.AddPolicy(
                    PolicyConstants.ViewOrders,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Manager));

                options.AddPolicy(
                    PolicyConstants.ManageOrderStatus,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Manager));
            });

    private static void CreateIndexes(IMongoDatabase database)
    {
        CreateIndexForCollection<Address>(database);
        CreateIndexForCollection<Answer>(database);
        CreateIndexForCollection<Cart>(database);
        CreateIndexForCollection<CartItem>(database);
        CreateIndexForCollection<Category>(database);
        CreateIndexForCollection<Comment>(database);
        CreateIndexForCollection<Order>(database);
        CreateIndexForCollection<OrderItem>(database);
        CreateIndexForCollection<Payment>(database);
        CreateIndexForCollection<Person>(database);
        CreateIndexForCollection<PhoneNumber>(database);
        CreateIndexForCollection<Product>(database);
        CreateIndexForCollection<Question>(database);
        CreateIndexForCollection<Role>(database);
        CreateIndexForCollection<Shipment>(database);
        CreateIndexForCollection<User>(database);
        CreateIndexForCollection<UserRole>(database);
        CreateIndexForCollection<Vote>(database);
    }

    private static void CreateIndexForCollection<TEntity>(IMongoDatabase database)
        where TEntity : BaseEntity
    {
        var collectionName = typeof(TEntity).Name.Pluralize();

        collectionName = collectionName == "People" ? "Persons" : collectionName;

        var collection = database.GetCollection<TEntity>(collectionName);

        var indexKeysDefinition = Builders<TEntity>.IndexKeys.Ascending(entity => entity.ExternalId);

        var indexModel = new CreateIndexModel<TEntity>(indexKeysDefinition);

        collection.Indexes.CreateOne(indexModel);
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