using System;
using System.Collections.Generic;

namespace CafeManagement.Model;

public partial class Order
{
    public int OrderId { get; set; }

    public int TableNumber { get; set; }

    public int CustomerCount { get; set; }

    public string OrderStatus { get; set; } = null!;

    public int? ShiftId { get; set; }

    public virtual ICollection<Orderdish> Orderdishes { get; } = new List<Orderdish>();

    public virtual Shift? Shift { get; set; }
}
