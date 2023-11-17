using Application;
using Infrastructure;
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

try
{
    builder
        .Services
        .AddEndpointsApiExplorer()
        .InjectApplicationLayer()
        .InjectInfrastructureLayer(builder.Configuration)
        .AddAllGraphQlServices()
        .AddLogger(builder.Configuration);

    var app = builder.Build();

    app.MapGraphQL();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();

        app.UseGraphQLVoyager("/graphql-voyager");
    }

/*
    app
        .UseHttpsRedirection();
        .UseCors("AllowAnyOrigin")
        .UseRouting()
        .UseAuthentication()
        .UseAuthorization()
        .UseEndpoints(endpoints => endpoints.MapControllers());
*/
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