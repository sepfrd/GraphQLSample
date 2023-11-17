using Microsoft.Extensions.Logging;
using Serilog.Context;
using Serilog.Core.Enrichers;
using Serilog.Events;
using ILogger = Serilog.ILogger;

namespace Infrastructure.Services.Logging;

public class CustomLogger(ILogger logger) : Microsoft.Extensions.Logging.ILogger
{
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

        logger.Write(serilogLogLevel, exception, message);
    }

    public bool IsEnabled(LogLevel logLevel) =>
        logLevel != LogLevel.None;

    public IDisposable? BeginScope<TState>(TState state)
        where TState : notnull =>
        LogContext.Push(new PropertyEnricher(nameof(state), state));
}