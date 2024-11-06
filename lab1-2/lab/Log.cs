using lab;
using Microsoft.AspNetCore.Diagnostics;
using NLog;

public class Log
{
    private static readonly Logger logger = LogManager.GetCurrentClassLogger();
    private static readonly bool isVerbose = ArgParser.HasArg(ArgParser.ARG_VERBOSE);

    public void Debug(string message, params object[] args)
    {
        if (!isVerbose)
        {
            return;
        }
        logger.Debug(string.Format(message, args));
    }

    public void Info(string message, params object[] args)
    {
        logger.Info(string.Format(message, args));
    }

    public void Console(string message, params object[] args)
    {
        logger.Trace(string.Format(message, args)); 
    }

    public void Error(string message, params object[] args)
    {
        logger.Error(string.Format(message, args));
    }
}