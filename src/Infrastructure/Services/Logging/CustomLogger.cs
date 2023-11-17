using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Context;
using Serilog.Core;
using Serilog.Core.Enrichers;
using Serilog.Enrichers;
using Serilog.Events;
using ILogger = Microsoft.Extensions.Logging.ILogger;
using RollingInterval = Serilog.Sinks.MongoDB.RollingInterval;

namespace Infrastructure.Services.Logging;

public class CustomLogger : ILogger
{
    private readonly Logger _logger = new LoggerConfiguration()
        .MinimumLevel.Debug()
        .WriteTo
        .MongoDBBson(configuration =>
        {
            configuration.SetConnectionString("mongodb://localhost:27017/OnlineShopSample");
            configuration.SetCollectionName("Logs");
            configuration.SetCreateCappedCollection(1024, 50_000);
            configuration.SetRollingInternal(RollingInterval.Day);
        })
        .Enrich
        .FromLogContext()
        .Enrich
        .With(
            new MachineNameEnricher(),
            new EnvironmentNameEnricher(),
            new EnvironmentUserNameEnricher())
        .CreateLogger();

    public void Log<TState>(
        LogLevel logLevel,
        EventId eventId,
        TState state,
        Exception? exception,
        Func<TState, Exception?, string> formatter)
    {
        if (!IsEnabled(logLevel))
        {
            return;
        }

        var message = formatter(state, exception);

        if (string.IsNullOrEmpty(message))
        {
            return;
        }

        LogEventLevel serilogLogLevel = logLevel switch
        {
            LogLevel.Trace => LogEventLevel.Verbose,
            LogLevel.Debug => LogEventLevel.Debug,
            LogLevel.Information => LogEventLevel.Information,
            LogLevel.Warning => LogEventLevel.Warning,
            LogLevel.Error => LogEventLevel.Error,
            LogLevel.Critical => LogEventLevel.Fatal,
            _ => LogEventLevel.Information
        };

        _logger.Write(serilogLogLevel, exception, message);
    }

    public bool IsEnabled(LogLevel logLevel) =>
        logLevel != LogLevel.None;

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull =>
        LogContext.Push(new PropertyEnricher(nameof(state), state));
}