﻿using System;
using System.Collections.Generic;

namespace OnlineShop.Models;

public partial class Category
{
    public int CategoryId { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Product> Product { get; set; } = new List<Product>();
}
