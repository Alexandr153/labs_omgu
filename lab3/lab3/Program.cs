using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;

namespace lab3;

public class Program
{
    public static void Main(string[] args)
    {
        string filePath = "employees.json";
        // Получаем значение первого аргумента
        string FirstArg = args[0];
        // Массив допустимых значений первого аргумента
        string[] FirstArgRules = [ArgParser.ARG_LIST, ArgParser.ARG_ADD, ArgParser.ARG_SHOW, ArgParser.ARG_UPDATE, ArgParser.ARG_REMOVE];
        if (string.IsNullOrWhiteSpace(FirstArg) | !FirstArgRules.Contains(FirstArg))
        {
            Console.WriteLine("Ошибка. Первый аргумент должен являться CRUD операцией:\n--list\n--add\n--show\n--update\n--remove");
            return;
        }
        // Проверка обязательных аргументов командной строки
        if (FirstArg == ArgParser.ARG_ADD | FirstArg == ArgParser.ARG_SHOW | FirstArg == ArgParser.ARG_REMOVE | FirstArg == ArgParser.ARG_UPDATE)
        {
            string email = ArgParser.GetArgAsString(ArgParser.ARG_EMAIL);
            if (string.IsNullOrWhiteSpace(email))
            {
                Console.WriteLine("Не указан обязательный аргумент --email");
                return;
            }
        }
        // Оператор --add
        if (FirstArg == ArgParser.ARG_ADD)
        {
            // Получаем данные всех аргументов 
            string Email = ArgParser.GetArgAsString(ArgParser.ARG_EMAIL);
            string FullName = ArgParser.GetArgAsString(ArgParser.ARG_NAME);
            if(string.IsNullOrEmpty(FullName)){
                FullName = "-";
            }
            string Phone = ArgParser.GetArgAsString(ArgParser.ARG_PHONE);
            if(string.IsNullOrEmpty(Phone)){
                Phone = "-";
            }
            string Hired = ArgParser.GetArgAsString(ArgParser.ARG_HIRED);
            // Проверяем на верный формат даты
            if (!string.IsNullOrEmpty(Hired)){
                try
                {
                    DateTime hired = DateTime.ParseExact(Hired, "dd.MM.yyyy", null);
                    Hired = hired.ToString();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Неверный формат даты.");
                    return;
                }
            }
            else{
                Hired = "-";
            }
            string Fired = ArgParser.GetArgAsString(ArgParser.ARG_FIRED);
            if (!string.IsNullOrEmpty(Fired)){
                try
                {
                    DateTime fired = DateTime.ParseExact(Fired, "dd.MM.yyyy", null);
                    Fired = fired.ToString();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Неверный формат даты.");
                    return;
                }
            }
            else{
                Fired = "-";
            }
            
            // Записываем в файл данные новой строкой
            var NewEmployee = new Employee(FullName, Email, Phone, Hired, Fired);        

            DataEngine fileManager = new(filePath);
            fileManager.AddEmployee(NewEmployee);

            Console.WriteLine("Данные успешно записаны");
            // Вывод на экран пользователя
            FormatTables.UserFormat(NewEmployee);
            return;
        }

        // Оператор --list
        else if (FirstArg == ArgParser.ARG_LIST)
        {
            DataEngine dataEngine = new(filePath);
            List<Employee> employees = dataEngine.LoadEmployees();
            if (employees != null && employees.Count > 0)
            {
                // Форматирование и вывод на экран данных
                FormatTables.ListFormat(employees);
                return;
            }
            else
            {
                Console.WriteLine("Список сотрудников пуст.");
                return;
            }
        }

        // Оператор --show
        else if (FirstArg == ArgParser.ARG_SHOW)
        {
            string Email = ArgParser.GetArgAsString(ArgParser.ARG_EMAIL);
            DataEngine dataEngine = new(filePath);
            List<Employee> employees = dataEngine.LoadEmployees();
            
            foreach(var employee in employees)
            {
                if (employee.Email == Email)
                {
                    FormatTables.UserFormat(employee);
                    return;
                }
                else {continue;}
            }
            Console.WriteLine($"Пользователь с почтой {Email} не найден");
        }

        // Оператор --update
        else if (FirstArg == ArgParser.ARG_UPDATE)
        {
            string Email = ArgParser.GetArgAsString(ArgParser.ARG_EMAIL);
            string FullName = ArgParser.GetArgAsString(ArgParser.ARG_NAME);
            string Phone = ArgParser.GetArgAsString(ArgParser.ARG_PHONE);
            string Hired = ArgParser.GetArgAsString(ArgParser.ARG_HIRED);
            // Проверяем на верный формат даты
            if (!string.IsNullOrEmpty(Hired)){
                try
                {
                    DateTime hired = DateTime.ParseExact(Hired, "dd.MM.yyyy", null);
                    Hired = hired.ToString();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Неверный формат даты.");
                    return;
                }
            }
            string Fired = ArgParser.GetArgAsString(ArgParser.ARG_FIRED);
            if (!string.IsNullOrEmpty(Fired)){
                try
                {
                    DateTime fired = DateTime.ParseExact(Fired, "dd.MM.yyyy", null);
                    Fired = fired.ToString();
                }
                catch (FormatException ex)
                {
                    Console.WriteLine("Неверный формат даты.");
                    return;
                }
            }

            DataEngine dataEngine = new(filePath);

            dataEngine.UpdateEmployee(new Dictionary<string, object>
            {
                ["Email"] = Email,
                ["FullName"] = FullName,
                ["Phine"] = Phone,
                ["Hired"] = Hired,
                ["Fired"] = Fired
            });

        
            // Вывод на экран конкретного пользователя
            List<Employee> employees = dataEngine.LoadEmployees();

            foreach(var employee in employees)
            {
                if (employee.Email == Email)
                {
                    FormatTables.UserFormat(employee);
                    return;
                }
                else {continue;}
            }
        }

        // Оператор --remove
        else if(FirstArg == ArgParser.ARG_REMOVE)
        {
            string Email = ArgParser.GetArgAsString(ArgParser.ARG_EMAIL);
            DataEngine dataEngine = new(filePath);
            dataEngine.DeleteEmployeeByEmail(Email);
        }
    }
}
