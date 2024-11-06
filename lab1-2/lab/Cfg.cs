using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab;

public class Cfg
{
    // Указываем константу - расположение файла
    public static readonly string APP_SETTINGS_PATH = "appsettings.json";

    private static IConfiguration config = null;

    static Log logger = new();

    // Подключаемся к конфиг файлу, если не получаетсяб возвращаем ошибку
    static Cfg() {
        try
        {
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.AddJsonFile(APP_SETTINGS_PATH, false);
            config = builder.Build();
            logger.Info("Configuration file compiled");
        }
        catch (TypeInitializationException ex)
        {
            logger.Error("Error building configuration file" + ex.Message);
            throw new TypeInitializationException("Configuration load error: "+ex.Message, ex);
        }
    }

    // Возвращаем значение по параметру из конфиг файла(в нашем случае там 1 значение)
    public static string ReadString(string paramName)
    {
        logger.Info("Configuration parameter returned");
        return config.GetSection(paramName).Value;
    }
}