using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class Product
{
    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }
    public string Status {  get; set; }
    public virtual ICollection<CartItem> CartItems { get; set; } = new List<CartItem>();

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
}
