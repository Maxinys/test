using System;
using System.Collections.Generic;
using System.Linq;

namespace CafeManagement.Model;

public partial class Employee
{
    public int EmployeeId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string Status { get; set; } = null!;

    public int? UserId { get; set; }

    public virtual ICollection<Shiftemployee> Shiftemployees { get; } = new List<Shiftemployee>();

    public virtual User? User { get; set; }
    
}
