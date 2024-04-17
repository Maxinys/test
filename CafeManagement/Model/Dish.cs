using System;
using System.Collections.Generic;
using System.Data;

namespace CafeManagement.Model;

public partial class Dish
{
    public int DishId { get; set; }

    public string DishName { get; set; } = null!;

    public decimal Price { get; set; }

    public byte[]? Photo { get; set; }

    public virtual ICollection<Orderdish> Orderdishes { get; } = new List<Orderdish>();
}
