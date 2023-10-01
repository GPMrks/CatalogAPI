namespace CatalogAPI.Logging;

[ProviderAlias("CustomLogger")]
public class CustomerLogger : ILogger
{
    private readonly string loggerName;
    private readonly CustomLoggerProviderConfiguration _loggerProviderConfiguration;

    public CustomerLogger(string loggerName, CustomLoggerProviderConfiguration loggerProviderConfiguration)
    {
        this.loggerName = loggerName;
        _loggerProviderConfiguration = loggerProviderConfiguration;
    }

    public IDisposable? BeginScope<TState>(TState state) where TState : notnull
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == _loggerProviderConfiguration.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state, Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string message = $"{logLevel.ToString()} : {eventId.Id} - {formatter(state, exception)}";
        
        WriteTextInFile(message);
    }

    private void WriteTextInFile(string message)
    {
        string pathLogFile = @"/home/gpmrks/CatalogAPI/Logs/Catalog_API_Log.txt";

        using (StreamWriter streamWriter = new StreamWriter(pathLogFile, true))
        {
            try
            {
                streamWriter.WriteLine(message);
                streamWriter.Close();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}