using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab3;

public class ArgParser
{
    // Константы указывающие на доступные аргументы
    public static readonly string ARG_COUNT = "--count";
    public static readonly string ARG_RELEASE = "--release";

    public static readonly string ARG_NAME = "--name"; 
    public static readonly string ARG_EMAIL = "--email";
    public static readonly string ARG_PHONE = "--phone";
    public static readonly string ARG_HIRED = "--hired";
    public static readonly string ARG_FIRED = "--fired";

    public static readonly string ARG_LIST = "--list";
    public static readonly string ARG_ADD = "--add";
    public static readonly string ARG_SHOW = "--show";
    public static readonly string ARG_UPDATE = "--update";
    public static readonly string ARG_REMOVE = "--remove";

    // Массив, который будет хранить полученные аргументы
    private static string[] _args = null;
    
    // Запись всех аргументов полученных из командной строки при запуске кода
    static ArgParser()
    {
        _args = Environment.GetCommandLineArgs();
    }

    // Запись всех аргументов вручную(для тестирования)
    public static void SetArgs(params string[] args)
    {
        _args = args;
    }

    // Проверка наличия указанного в параметре аргумента с полученным из командной строки(терминала)
    // Если параметр найден в аргументах командной строки, то возвращает True, иначе False(для тестирования)
    public static bool HasArg(string argName) 
    {
        for (int i = 0; i < _args.Length; i++)
        {
            if (string.Compare(_args[i], argName, ignoreCase:true) == 0)
            {
                return true;
            }
        }
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
                return _args[i+1];
            }
        }
        return null;
    }
}