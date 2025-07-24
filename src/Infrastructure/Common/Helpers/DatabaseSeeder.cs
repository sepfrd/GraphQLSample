using Bogus;
using Domain.Entities;
using Domain.Enums;
using Domain.ValueObjects;
using Infrastructure.Abstractions;
using Infrastructure.Common.Configurations;
using Infrastructure.Common.Constants;
using Infrastructure.Services.AuthService;
using MongoDB.Driver;

namespace Infrastructure.Common.Helpers;

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
        var fakeProjects = GetFakeProjects();
        var fakeDepartments = GetFakeDepartments();
        var fakeUsers = GetFakeUsers();

        foreach (var employee in fakeEmployees)
        {
            var randomFakeDepartment = fakeDepartments[Random.Shared.Next(0, fakeDepartments.Count)];
            employee.DepartmentId = randomFakeDepartment.Id;
        }

        for (var i = 0; i < _dataSeedOptions.ItemsCount; i++)
        {
            fakeProjects[i].ManagerId = fakeEmployees[Random.Shared.Next(0, fakeDepartments.Count)].Id;
            fakeProjects[i].EmployeeIds = [fakeEmployees[i].Id];
            fakeEmployees[i].ProjectIds = [fakeProjects[i].Id];
        }

        _mongoDatabase.GetCollection<Department>("Departments").InsertMany(fakeDepartments);
        _mongoDatabase.GetCollection<Employee>("Employees").InsertMany(fakeEmployees);
        _mongoDatabase.GetCollection<Project>("Projects").InsertMany(fakeProjects);
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

        for (var i = 0; i < _dataSeedOptions.ItemsLargeCount; i++)
        {
            fakeEmployees.Add(employeeFaker.Generate());
        }

        return fakeEmployees;
    }

    private List<Project> GetFakeProjects()
    {
        var projectFaker = new Faker<Project>()
            .RuleFor(
                project => project.Name,
                faker => faker.PickRandomParam("Financial", "Stocks", "Telecom", "CMS"))
            .RuleFor(
                project => project.Description,
                faker => faker.PickRandomParam("Project's Description"));

        var fakeProjects = new List<Project>();

        for (var i = 0; i < _dataSeedOptions.ItemsCount; i++)
        {
            fakeProjects.Add(projectFaker.Generate());
        }

        return fakeProjects;
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
        var admin = new User
        {
            Username = _dataSeedOptions.AdminUsername,
            PasswordHash = _authService.HashPassword(_dataSeedOptions.AdminPassword),
            Email = _dataSeedOptions.AdminEmail,
            IsEmailConfirmed = true,
            Roles = [RoleConstants.User, RoleConstants.Admin]
        };

        var user = new User
        {
            Username = _dataSeedOptions.UserUsername,
            PasswordHash = _authService.HashPassword(_dataSeedOptions.UserPassword),
            Email = _dataSeedOptions.UserEmail,
            IsEmailConfirmed = true,
            Roles = [RoleConstants.User]
        };

        return [admin, user];
    }
}