using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class Product
{
    public Product()
    {
    }

    public Product(int? categoryId, string name, decimal price, int quantityInStock)
    {
        CategoryId = categoryId;
        Name = name;
        Price = price;
        QuantityInStock = quantityInStock;
    }

    public int ProductId { get; set; }

    public int? CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public decimal Price { get; set; }

    public int QuantityInStock { get; set; }

    public virtual Category? Category { get; set; }

    public virtual ICollection<OrderItem> OrderItem { get; set; } = new List<OrderItem>();
}
