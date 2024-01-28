using Application;
using Application.Abstractions;
using Infrastructure;
using Infrastructure.Persistence.Common.Helpers;
using Infrastructure.Services.Logging;
using Serilog;
using Serilog.Settings.Configuration;
using Web;
using ILogger = Microsoft.Extensions.Logging.ILogger;
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
    builder
        .Services
        .AddHttpContextAccessor()
        .AddEndpointsApiExplorer()
        .AddSingleton<ILogger, CustomLogger>()
        .InjectApplicationLayer()
        .InjectInfrastructureLayer(builder.Configuration)
        .AddAllGraphQlServices()
        .AddHealthChecks();

    var app = builder.Build();

    if (builder.Configuration.GetSection("EnableDataSeed").Value == "True")
    {
        using var scope = app.Services.CreateScope();

        var serviceProvider = scope.ServiceProvider;

        var authenticationService = serviceProvider.GetRequiredService<IAuthenticationService>();

        var dataSeeder = new DatabaseSeeder(
            builder.Configuration.GetSection("MongoDb").GetSection("ConnectionString").Value!,
            builder.Configuration.GetSection("MongoDb").GetSection("DatabaseName").Value!,
            authenticationService);

        dataSeeder.SeedData();
    }

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        app.UseGraphQLVoyager("/graphql-voyager");
    }

    app
        // .UseHttpsRedirection()
        // .UseCors("AllowAnyOrigin")
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
            endpoints.MapHealthChecks("/healthcheck");
        });

    app.Run();
}
catch (Exception exception)
{
    Log.Error(exception, "Program stopped due to a {ExceptionType} exception", exception.GetType());

    throw;
}
finally
{
    Log.CloseAndFlush();
}