using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeManagement.Model;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public string Role { get; set; } = null!;

    public virtual ICollection<Employee> Employees { get; } = new List<Employee>();
    public string FIO
    {
        get
        {
            Employee emp = App.context.Employees.ToList().Find(u => u.UserId == UserId);
            return $"{emp.FirstName} {emp.LastName}";
        }

    }
    public string Id
    {
        get
        {
            Employee emp = App.context.Employees.ToList().Find(u => u.UserId == UserId);
            return $"{emp.EmployeeId}";
        }

    }
    public string dolgnost
    {
        get
        {
            return $"{Role}";

        }
    }
}
