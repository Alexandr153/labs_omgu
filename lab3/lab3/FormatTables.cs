using lab3;

public class FormatTables
{
    public static void ListFormat(List<Employee> employees)
    {
        // Ширина колонок
        int nameWidth = 15;
        int emailWidth = 20;
        int phoneWidth = 18;
        int hiredWidth = 20;
        int firedWidth = 20;
        // Заголовки таблицы
        Console.WriteLine($"| {"Name".PadRight(nameWidth)} | {"Email".PadRight(emailWidth)} | {"Phone".PadRight(phoneWidth)} | {"Hired".PadRight(hiredWidth)} | {"Fired".PadRight(firedWidth)} |");

        // Разделитель
        Console.WriteLine('|' + new string( '-', nameWidth + emailWidth + phoneWidth + hiredWidth + firedWidth + 14) + '|');


        // Обработка списка сотрудников
        foreach (var employee in employees)
        {
            Console.WriteLine($"| {employee.FullName.PadRight(nameWidth)} |" +
                              $"{employee.Email.PadRight(emailWidth)} |" +
                              $"{employee.Phone.PadRight(phoneWidth)} |" +
                              $"{employee.Hired.PadRight(hiredWidth)} |" +
                              $"{employee.Fired.PadRight(firedWidth)} |"
                              );
        }
        Console.WriteLine('|' + 
                         new string( '-', nameWidth + emailWidth + 
                                          phoneWidth + hiredWidth + 
                                          firedWidth + 14) + 
                          '|'
                          );
    }

    public static void UserFormat(Employee employee)
    {
        Console.WriteLine($"\n\n" +
                          $"Name: {employee.FullName}\n" +
                          $"Email: {employee.Email}\n" +
                          $"Phone: {employee.Phone}\n" +
                          $"Hired: {employee.Hired}\n" +
                          $"Fired: {employee.Fired}"
                          );
    }
}