using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class Order
{
    public int OrderId { get; set; }

    public int? AccountId { get; set; }

    public decimal TotalAmount { get; set; }

    public string Status { get; set; } = null!;

    public DateTime? OrderDate { get; set; }

    public virtual Account? Account { get; set; }

    public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
}
