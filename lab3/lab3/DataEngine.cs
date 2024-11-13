using Newtonsoft.Json;
using lab3;

public class DataEngine
{
    private readonly string _filePath;

    // Конструктор
    public DataEngine(string filePath)
    {
        _filePath = filePath;
    }

    // Метод для добавления нового сотрудника в список
    public void AddEmployee(Employee employee)
    {
        var jsonString = JsonConvert.SerializeObject(employee);
        jsonString += "\n";  
        File.AppendAllText(_filePath, jsonString);
    }
    
    // Метод для обновления информации о сотруднике из списка
    public void UpdateEmployee(Dictionary<string, object> updates)
    {
         if (!File.Exists(_filePath))
         {
            Console.WriteLine("Файл базы данных пуст");
         }
         // Чтение содержимого файла
        string json = File.ReadAllText(_filePath);

        List<Employee> employees = this.LoadEmployees();

        foreach (var employee in employees)
        {
            if (employee.Email.Equals(updates["Email"]))
            {
                // Обновление свойств
                foreach (var update in updates)
                {
                    switch (update.Key) 
                    {
                        case "Email":
                            employee.Email = update.Value == null ? employee.Email : (string)update.Value;
                            break;
                        case "FullName":
                            employee.FullName = update.Value == null ? employee.FullName : (string)update.Value;
                            break;
                        case "Phone":
                            employee.Phone = update.Value == null ? employee.Phone : (string)update.Value;
                            break;
                        case "Hired":
                            employee.Hired = update.Value == null ? employee.Hired : (string)update.Value;
                            break;
                        case "Fired":
                            employee.Fired = update.Value == null ? employee.Fired : (string)update.Value;
                            break;
                    }
                }

                break; // Если нашли сотрудника, то прекращаем поиск после замены
            }
        }


        // Сериализация списка обратно в строку JSON
        string updatedJson = JsonConvert.SerializeObject(employees).Replace("},", "}\n").Replace("[", "").Replace("]", "");
        File.WriteAllText(_filePath, updatedJson);
        Console.WriteLine("Информация обновлена");
    }

    // Метод для удаления сотрудника из списка
    public void DeleteEmployeeByEmail(string email)
    {
        if (!File.Exists(_filePath))
        {
            Console.WriteLine("Файл базы данных пуст");
            return;
        }

        // Загрузка списка сотрудников из файла
        List<Employee> employees = this.LoadEmployees();

        // Удаляем сотрудника с указанным email
        Employee employeeToRemove = employees.Find(e => e.Email == email);
        if (employeeToRemove != null)
        {
            employees.Remove(employeeToRemove);
            Console.WriteLine($"Сотрудник с email {email} был успешно удалён.");
        }
        else
        {
            Console.WriteLine($"Сотрудника с email {email} не найдено.");
        }

        // Сериализуем оставшихся сотрудников обратно в файл
        string updatedJson = JsonConvert.SerializeObject(employees).Replace("},", "}\n").Replace("[", "").Replace("]", "");
        File.WriteAllText(_filePath, updatedJson);
    }

    // Метод для загрузки списка сотрудников из списка
    public List<Employee> LoadEmployees()
    {
        if (!File.Exists(_filePath)) return null;
        
        var employees = new List<Employee>();

        // Читаем файл построчно
        foreach (var line in File.ReadLines(_filePath))
        {
            // Десериализация каждой строки в объект Employee
            var employee = JsonConvert.DeserializeObject<Employee>(line);
            employees.Add(employee);
        }

        return employees;
    }

}