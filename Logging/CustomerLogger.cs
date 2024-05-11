
public class CustomerLogger : ILogger
{
    readonly string loggerName;
    readonly CustomLoggerProviderConfiguration loggerConfig;

    public CustomerLogger(string name, CustomLoggerProviderConfiguration config)
    {
        loggerName = name;
        loggerConfig = config;
    }

    public IDisposable? BeginScope<TState>(TState state)
    {
        return null;
    }

    public bool IsEnabled(LogLevel logLevel)
    {
        return logLevel == loggerConfig.LogLevel;
    }

    public void Log<TState>(LogLevel logLevel, EventId eventId, TState state,
             Exception? exception, Func<TState, Exception?, string> formatter)
    {
        string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state, exception)}";
        EscreverTextoNoArquivo(mensagem);
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        try
        {
            string caminhoArquivoLog = @"./barbearia_log.txt";

            if (!File.Exists(caminhoArquivoLog))
            {

                File.Create(caminhoArquivoLog);
            }

            using (StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true))
            {                
                streamWriter.WriteLine(mensagem);
                streamWriter.Close();
            }

        }
        catch (Exception ex)
        {
            throw;
        }

    }
}