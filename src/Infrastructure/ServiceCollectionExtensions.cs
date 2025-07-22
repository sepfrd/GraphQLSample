using System.Security.Cryptography;
using Application.Common.Abstractions;
using Application.Common.Abstractions.Services;
using Application.Common.Dtos;
using Application.Services;
using Domain.Abstractions;
using Domain.Entities;
using FluentValidation;
using FluentValidation.AspNetCore;
using Humanizer;
using Infrastructure.Abstraction;
using Infrastructure.Common.Configurations;
using Infrastructure.Common.Constants;
using Infrastructure.Persistence.Repositories;
using Infrastructure.Services.AuthService;
using Infrastructure.Services.AuthService.Dtos.UserDto;
using Infrastructure.Services.Mapping;
using Mapster;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Serializers;
using MongoDB.Driver;

namespace Infrastructure;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, AppOptions appOptions)
    {
        ConfigureMapster();

        return services
            .AddScoped<IAuthService, AuthService>()
            .AddScoped<IRepositoryBase<Employee>, EmployeeRepository>()
            .AddScoped<IRepositoryBase<User>, UserRepository>()
            .AddScoped<IServiceBase<EmployeeDto>, ServiceBase<Employee, EmployeeDto>>()
            .AddFluentValidation()
            .AddMongoDb(appOptions.MongoDbOptions)
            .AddSingleton<IMappingService, MappingService>()
            .AddScoped<IAuthenticationService, AuthenticationService>()
            .AddAuth(appOptions.JwtOptions);
    }

    private static IServiceCollection AddFluentValidation(this IServiceCollection services) =>
        services
            .AddFluentValidationAutoValidation()
            .AddValidatorsFromAssemblyContaining<UserDtoValidator>();

    private static IServiceCollection AddMongoDb(this IServiceCollection services, MongoDbOptions mongoDbOptions)
    {
        BsonSerializer.RegisterSerializer(new GuidSerializer(BsonType.String));

        BsonClassMap.RegisterClassMap<DomainEntity>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdMember(baseEntity => baseEntity.Uuid);
        });

        BsonClassMap.RegisterClassMap<User>(classMap =>
        {
            classMap.AutoMap();
            classMap.MapIdMember(user => user.Uuid);
        });

        var connectionString = mongoDbOptions.ConnectionString;
        var databaseName = mongoDbOptions.DatabaseName;

        var mongoClient = new MongoClient(connectionString);

        var mongoDatabase = mongoClient.GetDatabase(databaseName);

        CreateIndexes(mongoDatabase);

        return services;
    }

    private static void ConfigureMapster()
    {
        TypeAdapterConfig<Employee, EmployeeDto>
            .ForType()
            .Map(
                employeeDto => employeeDto.FirstName,
                employee => employee.Info.FirstName)
            .Map(
                employeeDto => employeeDto.LastName,
                employee => employee.Info.LastName);
    }

    public static IServiceCollection AddAuth(this IServiceCollection services, JwtOptions jwtOptions) =>
        services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                var rsa = RSA.Create();

                rsa.ImportFromPem(jwtOptions.PublicKey);

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
                    ValidIssuer = jwtOptions.Issuer,
                    ValidAudience = jwtOptions.Audience
                };
            })
            .Services
            .AddAuthorization(options =>
            {
                options.AddPolicy(
                    PolicyConstants.CustomerPolicy,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Customer, RoleConstants.Admin));

                options.AddPolicy(
                    PolicyConstants.AdminPolicy,
                    policyConfig => policyConfig.RequireRole(RoleConstants.Admin));
            });

    private static void CreateIndexes(IMongoDatabase database)
    {
        CreateIndexForCollection<Employee>(database);
        CreateIndexForCollection<Department>(database);
    }

    private static void CreateIndexForCollection<TEntity>(IMongoDatabase database)
        where TEntity : DomainEntity
    {
        var collectionName = typeof(TEntity).Name.Pluralize();

        collectionName = collectionName == "People" ? "Persons" : collectionName;

        var collection = database.GetCollection<TEntity>(collectionName);

        var indexKeysDefinition = Builders<TEntity>.IndexKeys.Ascending(entity => entity.Uuid);

        var indexModel = new CreateIndexModel<TEntity>(indexKeysDefinition);

        collection.Indexes.CreateOne(indexModel);
    }
}