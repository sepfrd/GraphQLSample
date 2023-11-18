using Application;
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

try
{
    builder
        .Services
        .AddEndpointsApiExplorer()
        .InjectApplicationLayer()
        .InjectInfrastructureLayer(builder.Configuration)
        .AddAllGraphQlServices()
        .AddSingleton<ILogger, CustomLogger>();

    var app = builder.Build();

    if (builder.Configuration.GetSection("EnableDataSeed").Value == "True")
    {
        var dataSeeder = new DatabaseSeeder(
            builder.Configuration.GetSection("MongoDb").GetSection("ConnectionString").Value!,
            builder.Configuration.GetSection("MongoDb").GetSection("DatabaseName").Value!);
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
        // .UseAuthentication()
        // .UseAuthorization()
        .UseEndpoints(endpoints =>
        {
            endpoints.MapGraphQL();
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