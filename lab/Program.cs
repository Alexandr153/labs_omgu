using System;

namespace lab;

public class Program
{
    public static void Main(string[] args)
        {
            // Получаем значение аргумента --name
            string name = ArgParser.GetArgAsString(ArgParser.ARG_NAME);
            // Если значение отсутствует, то кидаем ошибку
            if (string.IsNullOrWhiteSpace(name))
            {
                Console.WriteLine("ERROR: Name not specified. Please, specify --name argument.");
                return;
            }
            // Получаем значение аргумента --hired
            string sHired = ArgParser.GetArgAsString(ArgParser.ARG_HIRED);
            // Если значение отсутствует, то кидаем ошибку
            if (string.IsNullOrWhiteSpace(sHired))
            {
                Console.WriteLine("ERROR: Hired date not specified. Please, specify --hired argument.");
                return;
            }
            try
            {
                // Объявляем переменную с типом данных DateTime, кладем в нее распаршенную версию 
                // переменной sHired, если не получилось распарсить, то присваиваем null
                DateTime hired = DateTime.ParseExact(sHired, "dd.MM.yyyy", null);
                // Присваиваем переменной message значение Message из файла appsettings.json
                string message = Cfg.ReadString("Message");
                // Заменяем в строке данные на наши полученные аргументы командной строки
                message = message.Replace("%name%", name);
                message = message.Replace("%hired%", hired.ToString("dd.MM.yyyy"));
                // Выводи на экран получившееся сообщение
                Console.WriteLine(message);
            }
            // Обрабатываем исключение FormatException, которое выбрасывается в случае неудачной попытки парсинга полученной даты(неверный формат даты)
            catch (FormatException ex)
            { 
                Console.WriteLine("ERROR: Hired date is incorrect. Please, use the format DD.MM.YYYY. ({0})", ex.Message);
                return;
            }            
        }
}
