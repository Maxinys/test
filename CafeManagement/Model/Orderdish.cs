using System;
using System.Collections.Generic;

namespace CafeManagement.Model;

public partial class Orderdish
{
    public int OrderDishId { get; set; }

    public int? OrderId { get; set; }

    public int? DishId { get; set; }

    public int Quantity { get; set; }

    public virtual Dish? Dish { get; set; }

    public virtual Order? Order { get; set; }
}
