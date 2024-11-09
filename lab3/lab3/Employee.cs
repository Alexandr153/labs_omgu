using lab3;

public class Employee
{
    public string Email { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Hired { get; set; }
    public string Fired { get; set; }

    public Employee(string fullname, string email, string phone, string hired, string fired)
    {
        FullName = fullname;
        Email = email;
        Phone = phone;
        Hired = hired;
        Fired = fired;
    }
}