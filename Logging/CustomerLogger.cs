
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
        
        try{
            string mensagem = $"{logLevel.ToString()}: {eventId.Id} - {formatter(state,exception)}";
            EscreverTextoNoArquivo(mensagem);
        }catch(Exception ex){
            Console.WriteLine($"Erro ao escrever no arquivo de log: {ex.Message}");
        }
    }

    private void EscreverTextoNoArquivo(string mensagem)
    {
        string caminhoArquivoLog= @"./barbearia_log.txt";

        using(StreamWriter streamWriter = new StreamWriter(caminhoArquivoLog, true)){

            try{

                streamWriter.WriteLine(mensagem);
                streamWriter.Close();

            }catch(Exception ex){

                throw;
            }
        }
    }
}