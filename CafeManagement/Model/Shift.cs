using System;
using System.Collections.Generic;

namespace CafeManagement.Model;

public partial class Shift
{
    public int ShiftId { get; set; }

    public string ShiftType { get; set; } = null!;

    public DateTime StartTime { get; set; }

    public DateTime EndTime { get; set; }

    public virtual ICollection<Order> Orders { get; } = new List<Order>();

    public virtual ICollection<Shiftemployee> Shiftemployees { get; } = new List<Shiftemployee>();
}
