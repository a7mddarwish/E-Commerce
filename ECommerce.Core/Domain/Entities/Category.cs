﻿using System;
using System.Collections.Generic;

namespace ECommerce.Core.Domain.Entities;

public partial class Category
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

  //B  public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
