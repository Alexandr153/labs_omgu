using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using NLog.Extensions.Logging;

namespace lab;

public class ArgParser
{
    // Константы указывающие на доступные аргументы
    public static readonly string ARG_NAME = "--name"; 
    public static readonly string ARG_HIRED = "--hired";
    public static readonly string ARG_VERBOSE = "--verbose";

    // Массив, который будет хранить полученные аргументы
    private static string[] _args = null;
    static Log logger = new();
    
    // Запись всех аргументов полученных из командной строки при запуске кода
    static ArgParser()
    {
        _args = Environment.GetCommandLineArgs();
        logger.Info($"Loaded {_args.Length} args from command line");
        logger.Info("Args loaded: " + String.Join(";", _args));
    }

    // Запись всех аргументов вручную(для тестирования)
    public static void SetArgs(params string[] args)
    {
        _args = args;
        logger.Info($"Loaded {_args.Length} args");
        logger.Info("Args loaded: " + String.Join(";", _args));
    }

    // Проверка наличия указанного в параметре аргумента с полученным из командной строки(терминала)
    // Если параметр найден в аргументах командной строки, то возвращает True, иначе False(для тестирования)
    public static bool HasArg(string argName) 
    {
        for (int i = 0; i < _args.Length; i++)
        {
            if (string.Compare(_args[i], argName, ignoreCase:true) == 0)
            {
                logger.Info($"Arg {argName} was found");
                return true;
            }
        }
        logger.Info($"The specified argument {argName} was not found");
        return false;
    }

    // Если указанный в параметр аргумент присутствует, то возвращает его значение
    // Если аргумент не указан, то возвращает null
    public static string GetArgAsString(string argName)
    {
        for (int i = 0; i < _args.Length; i++)
        {
            if (string.Compare(_args[i], argName, ignoreCase: true) == 0)
            {
                if (i + 1 > _args.Length - 1)
                    return null;
                logger.Info($"Argument value {argName} returned");
                return _args[i+1];
            }
        }
        logger.Info("No argument value was returned");
        return null;
    }
}