using NLog;

public class Log
{
    private static Logger _logger = LogManager.GetCurrentClassLogger();

    public void Debug(string message)
    {
        _logger.Debug(message);
    }

    public void Info(string message)
    {
        _logger.Info(message);
    }

    public void Console(string message)
    {
        _logger.Trace(message); 
    }

    public void Error(string message)
    {
        _logger.Error(message);
    }
}