using Application.Abstractions;
using GraphQL.Server.Ui.GraphiQL;
using HotChocolate.AspNetCore;
using Infrastructure.Common.Configurations;
using Infrastructure.Persistence.Common.Helpers;
using Microsoft.Extensions.Options;
using Serilog;
using Serilog.Settings.Configuration;
using Web;

var builder = WebApplication.CreateBuilder(args);

builder.Logging.ClearProviders();

Log.Logger = new LoggerConfiguration()
    .ReadFrom
    .Configuration(
        builder.Configuration,
        new ConfigurationReaderOptions
        {
            SectionName = "InternalSerilog"
        })
    .CreateBootstrapLogger();

Log.Logger.Information("Application setup started.");

try
{
    builder.Services.AddDependencies(builder.Configuration, builder.Environment);

    var app = builder.Build();

    var appOptions = app.Services.GetRequiredService<IOptions<AppOptions>>().Value;

    if (appOptions.DataSeedOptions.ShouldSeed)
    {
        using var scope = app.Services.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();

        var dataSeeder = new DatabaseSeeder(
            appOptions.MongoDbOptions,
            appOptions.DataSeedOptions,
            authenticationService);

        dataSeeder.SeedData();

        Console.BackgroundColor = ConsoleColor.DarkGreen;
        Console.ForegroundColor = ConsoleColor.Black;
        Console.WriteLine("Successfully seeded the database.");
        Console.ResetColor();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
    }

    app
        .UseRouting()
        .UseCors()
        .UseAuthentication()
        .UseAuthorization()
        .UseWebSockets()
        .UseEndpoints(endpoints =>
        {
            endpoints
                .MapGraphQL()
                .WithOptions(new GraphQLServerOptions
                {
                    Tool =
                    {
                        Enable = false
                    }
                });

            endpoints.MapHealthChecks("/health");
        })
        .UseGraphQLGraphiQL(options: new GraphiQLOptions
        {
            GraphQLEndPoint = appOptions.ApplicationUrl + "graphql"
        });

    await app.RunAsync();
}
catch (Exception exception)
{
    Log.Error(exception, "Program stopped due to a {ExceptionType} exception", exception.GetType());
}
finally
{
    await Log.CloseAndFlushAsync();
}