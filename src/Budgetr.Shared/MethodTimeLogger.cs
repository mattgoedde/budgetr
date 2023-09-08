using Microsoft.Extensions.Logging;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace Budgetr.Shared;

public sealed class MethodTimeLogger : IDisposable
{
    private readonly Type _type;
    private readonly ILogger? _logger;
    private readonly LogLevel _logLevel;
    private readonly string _methodName;
    private readonly string? _message;
    private readonly Stopwatch _stopwatch;

    /// <summary>
    /// Automatically logs the elapsed time of the scope it is inside of
    /// </summary>
    /// <param name="logger">The logger to use</param>
    /// <param name="logLevel">The level in which to log the message</param>
    /// <param name="message">An optional extension of the message</param>
    /// <param name="methodName">Filled in by the runtime</param>
    public MethodTimeLogger(Type type, ILogger? logger, LogLevel logLevel = LogLevel.Trace, string? message = null, [CallerMemberName] string methodName = "")
    {
        _type = type;
        _logger = logger;
        _logLevel = logLevel;
        _message = message;
        _methodName = methodName;
        _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        if (_message is null)
            _logger?.Log(_logLevel, "{class}.{method} {duration}", _type.Name, _methodName, _stopwatch.Elapsed);
        else
            _logger?.Log(_logLevel, "{class}.{method} {duration} - {message}", _type.Name, _methodName, _stopwatch.Elapsed, _message);
    }
}


/// <summary>
/// Automatically logs the elapsed time of the scope it is inside of
/// </summary>
/// <typeparam name="TClass">Enclosing class</typeparam>
public sealed class MethodTimeLogger<TClass> : IDisposable
{
    private readonly ILogger<TClass>? _logger;
    private readonly LogLevel _logLevel;
    private readonly string _methodName;
    private readonly string? _message;
    private readonly Stopwatch _stopwatch;

    /// <summary>
    /// Automatically logs the elapsed time of the scope it is inside of
    /// </summary>
    /// <param name="logger">The logger to use</param>
    /// <param name="logLevel">The level in which to log the message</param>
    /// <param name="message">An optional extension of the message</param>
    /// <param name="methodName">Filled in by the runtime</param>
    public MethodTimeLogger(ILogger<TClass>? logger, LogLevel logLevel = LogLevel.Trace, string? message = null, [CallerMemberName] string methodName = "")
    {
        _logger = logger;
        _logLevel = logLevel;
        _message = message;
        _methodName = methodName;
        _stopwatch = Stopwatch.StartNew();
    }

    public void Dispose()
    {
        _stopwatch.Stop();
        if (_message is null)
            _logger?.Log(_logLevel, "{class}.{method} {duration}", typeof(TClass).Name, _methodName, _stopwatch.Elapsed);
        else
            _logger?.Log(_logLevel, "{class}.{method} {duration} - {message}", typeof(TClass).Name, _methodName, _stopwatch.Elapsed, _message);
    }
}


