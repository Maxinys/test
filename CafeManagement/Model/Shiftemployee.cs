using System;
using System.Collections.Generic;

namespace CafeManagement.Model;

public partial class Shiftemployee
{
    public int ShiftEmployeeId { get; set; }

    public int? ShiftId { get; set; }

    public int? EmployeeId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Shift? Shift { get; set; }
}
