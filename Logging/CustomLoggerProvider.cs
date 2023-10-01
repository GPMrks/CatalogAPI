using System.Collections.Concurrent;

namespace CatalogAPI.Logging;

public class CustomLoggerProvider : ILoggerProvider
{
    private readonly CustomLoggerProviderConfiguration _loggerProviderConfiguration;

    private readonly ConcurrentDictionary<string, CustomerLogger> _loggers = new();

    public CustomLoggerProvider(CustomLoggerProviderConfiguration loggerProviderConfiguration)
    {
        _loggerProviderConfiguration = loggerProviderConfiguration;
    }

    public ILogger CreateLogger(string categoryName)
    {
        return _loggers.GetOrAdd(categoryName, name => new CustomerLogger(name, _loggerProviderConfiguration));
    }
    
    public void Dispose()
    {
        _loggers.Clear();
    }
}