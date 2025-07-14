using Bogus;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Abstraction;
using Infrastructure.Common.Configurations;
using Infrastructure.Services.AuthService;
using MongoDB.Driver;

namespace Infrastructure.Persistence.Common.Helpers;

public class DatabaseSeeder
{
    private readonly DataSeedOptions _dataSeedOptions;
    private readonly IAuthService _authService;
    private readonly IMongoDatabase _mongoDatabase;

    private static readonly Skill[] Skills =
    [
        new("C#"),
        new(".NET"),
        new("JavaScript"),
        new("TypeScript"),
        new("SQL Server"),
        new("Postgres"),
        new("MongoDB"),
        new("Redis"),
        new("React"),
        new("RabbitMQ"),
        new("Kafka"),
        new("CSS"),
        new("HTML"),
        new("Tailwind"),
        new("Bootstrap"),
        new("ElasticSearch")
    ];

    public DatabaseSeeder(
        MongoDbOptions mongoDbOptions,
        DataSeedOptions dataSeedOptions,
        IAuthService authService)
    {
        var mongoClient = new MongoClient(mongoDbOptions.ConnectionString);

        _mongoDatabase = mongoClient.GetDatabase(mongoDbOptions.DatabaseName);
        _dataSeedOptions = dataSeedOptions;
        _authService = authService;
    }

    public void SeedData()
    {
        var fakeEmployees = GetFakeEmployees();
        var fakeDepartments = GetFakeDepartments();
        var fakeUsers = GetFakeUsers();

        for (var i = 0; i < _dataSeedOptions.ItemsCount; i++)
        {
            fakeEmployees[i].DepartmentUuid = fakeDepartments[Random.Shared.Next(0, fakeDepartments.Count)].Uuid;
        }

        _mongoDatabase.GetCollection<Department>("Departments").InsertMany(fakeDepartments);
        _mongoDatabase.GetCollection<Employee>("Employees").InsertMany(fakeEmployees);
        _mongoDatabase.GetCollection<User>("Users").InsertMany(fakeUsers);
    }

    private List<Employee> GetFakeEmployees()
    {
        var infoFaker = new Faker<PersonInfo>()
            .CustomInstantiator(faker =>
                new PersonInfo(
                    faker.Person.FirstName,
                    faker.Person.LastName,
                    faker.Person.DateOfBirth.AddYears(-18)));

        var employeeFaker = new Faker<Employee>()
            .RuleFor(
                employee => employee.Info,
                () => infoFaker.Generate())
            .RuleFor(
                employee => employee.Position,
                faker => faker.PickRandomParam("Frontend Developer", "Backend Developer", "DevOps", "DBA", "CTO", "CEO"))
            .RuleFor(
                employee => employee.Status,
                faker => faker.PickRandom<EmployeeStatus>())
            .RuleFor(
                employee => employee.Skills,
                faker => [faker.PickRandom(Skills)]);

        var fakeEmployees = new List<Employee>();

        for (var i = 0; i < _dataSeedOptions.ItemsCount; i++)
        {
            fakeEmployees.Add(employeeFaker.Generate());
        }

        return fakeEmployees;
    }

    private static List<Department> GetFakeDepartments() =>
    [
        new()
        {
            Name = "Devs"
        },
        new()
        {
            Name = "HR"
        },
        new()
        {
            Name = "Management"
        }
    ];

    private List<User> GetFakeUsers()
    {
        var defaultUser = new User
        {
            Username = "sepehr_frd",
            PasswordHash = _authService.HashPassword("Correct_p0"),
            Email = "sepfrd@outlook.com",
            IsEmailConfirmed = true
        };

        var userFaker = new Faker<User>()
            .RuleFor(user => user.Username, faker => faker.Internet.UserName())
            .RuleFor(user => user.PasswordHash,
                faker => _authService.HashPassword(faker.Internet.Password()))
            .RuleFor(user => user.Email, faker => faker.Internet.Email())
            .RuleFor(user => user.IsEmailConfirmed, faker => faker.Random.Bool());

        var fakeUsers = new List<User>
        {
            defaultUser
        };

        for (var i = 0; i < _dataSeedOptions.ItemsCount - 1; i++)
        {
            fakeUsers.Add(userFaker.Generate());
        }

        return fakeUsers;
    }
}